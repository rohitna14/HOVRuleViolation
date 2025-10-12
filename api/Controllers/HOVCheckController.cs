using Microsoft.AspNetCore.Mvc;
using HOVLaneViolation.Data;
using HOVLaneViolation.Entities;
using Microsoft.EntityFrameworkCore;

namespace HOVLaneViolation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HOVCheckController : ControllerBase
    {
        private readonly DataContext _context;

        public HOVCheckController(DataContext context)
        {
            _context = context;
        }

        // Request/Response classes
        public class CheckRequest
        {
            public string Make { get; set; } = string.Empty;
            public string Model { get; set; } = string.Empty;
            public double Weight { get; set; }
            public string LicensePlate { get; set; } = string.Empty;
        }

        public class CheckResponse
        {
            public bool IsViolation { get; set; }
            public string Message { get; set; } = string.Empty;
            public HOVCustomerData? Customer { get; set; }
            public int MastId { get; set; }
            public int CustomerId { get; set; }
            public int MinWeight { get; set; }
        }

        public class ErrorResponse
        {
            public string Message { get; set; } = string.Empty;
            public string Code { get; set; } = string.Empty;
            public int? MinWeight { get; set; }
        }


        // Method 1: Validate Request
        private bool ValidateRequest(CheckRequest request, out ActionResult<CheckResponse> error)
        {
            if (request.Weight <= 0)
            {
                error = BadRequest(new ErrorResponse
                {
                    Message = "Weight must be positive",
                    Code = "INVALID_WEIGHT"
                });
                return false;
            }   
            error = null;
            return true;
        }

        // Method 2: Fetch Vehicle Data
        private async Task<(HOVMasterData? data, ActionResult<CheckResponse>? error)> FetchVehicleData(CheckRequest request)
        {
            var vehicle = await _context.HOVMasters
                .FirstOrDefaultAsync(m =>
                    m.Make.ToLower() == request.Make.ToLower() &&
                    m.Model.ToLower() == request.Model.ToLower());

            if (vehicle == null)
                return (null, NotFound(new ErrorResponse
                {
                    Message = "Vehicle not found",
                    Code = "VEHICLE_NOT_FOUND"
                }));

            if (request.Weight < vehicle.Weight)
                return (null, BadRequest(new ErrorResponse
                {
                    Message = $"Weight below minimum ({vehicle.Weight}lbs)",
                    Code = "WEIGHT_BELOW_MINIMUM",
                    MinWeight = vehicle.Weight
                }));

            return (vehicle, null);
        }

        // Method 3: Fetch Violation Rule
        private async Task<(HOVRule data, ActionResult<CheckResponse>? error)> FetchViolationRule()
        {
            var rule = await _context.HOVRules.FirstOrDefaultAsync();
            return rule == null
                ? (null, StatusCode(500, new ErrorResponse
                {
                    Message = "Rules not configured",
                    Code = "MISSING_RULES"
                }))
                : (rule, null);
        }

        // Method 4: Check for Violation
        private async Task<(bool isViolation, HOVCustomerData? customer)> CheckForViolation(
            CheckRequest request, HOVMasterData vehicle, HOVRule rule)
        {
            var isViolation = (request.Weight - vehicle.Weight) <= rule.Value;
            HOVCustomerData? customer = null;

            if (isViolation)
            {
                customer = await _context.HOVCustomers
                    .FirstOrDefaultAsync(c => c.LicensePlate.ToLower() == request.LicensePlate.ToLower());

                if (customer == null)
                    throw new Exception("Customer not found"); // Will be caught by global try-catch
            }

            return (isViolation, customer);
        }

        // Method 5: Process Final Result
        private async Task<ActionResult<CheckResponse>> ProcessResult(
            HOVMasterData vehicle, bool isViolation, HOVCustomerData? customer)
        {
            _context.HOVTransactions.Add(new HOVTransactionData
            {
                Date = DateTime.Now,
                HOVMasterId = vehicle.Id,
                ViolationStatus = isViolation,
                HOVCustomerId = isViolation ? customer?.Id : null
            });
            await _context.SaveChangesAsync();

            return Ok(new CheckResponse
            {
                IsViolation = isViolation,
                Message = isViolation ? "⚠️ HOV Violation Detected" : "✅ No Violation Detected",
                Customer = customer,
                MastId = vehicle.Id,
                CustomerId = customer?.Id ?? 0,
                MinWeight = vehicle.Weight
            });

        }
        [HttpPost("validate")]
        public async Task<ActionResult<CheckResponse>> CheckViolation([FromBody] CheckRequest request)
        {
            try
            {
                // 1. Validate request
                if (!ValidateRequest(request, out var error)) return error;
                
                // 2. Get vehicle data
                var vehicle = await FetchVehicleData(request);
                if (vehicle.error != null) return vehicle.error;
                
                // 3. Get violation rule
                var rule = await FetchViolationRule();
                if (rule.error != null) return rule.error;
                
                // 4. Check for violation
                var (isViolation, customer) = await CheckForViolation(request, vehicle.data!, rule.data!);
                
                // 5. Process and return result
                return await ProcessResult(vehicle.data!, isViolation, customer);
            }
            catch
            {
                return StatusCode(500, new ErrorResponse { 
                    Message = "An error occurred", 
                    Code = "SERVER_ERROR" 
                });
            }
        }

    }
}