using MathAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MathAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MathController : Controller
    {
        private readonly MathDbContext _context;

        public MathController(MathDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates and performs a MathCalculation
        /// </summary>
        /// <param name="FirstNumber">First number of the operation</param>
        /// <param name="SecondNumber">Second number of the operation</param>
        /// <param name="Operation">Operation as an integer: 1 is +, 2 is -, 3 is *, 4 is /</param>
        /// <param name="Token">Token of the current user.</param>
        /// <returns>A MathCalculation object with the result</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /PostCalulate
        ///     {
        ///        "FirstNumber": 5,
        ///        "SecondNumber": 5,
        ///        "Operation": 10,
        ///        "Token": "{Insert token here}"
        ///     }
        /// </remarks>
        /// <response code="201">Returns the newly created calculation</response>
        /// <response code="400">Returns if a request is missing details or fails</response>

        [HttpPost("PostCalculate")]
        [ProducesResponseType(typeof(MathCalculation), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Error),StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public async Task<IActionResult> PostCalculate(MathCalculation mathCalculation)
        {
            if (mathCalculation.FirebaseUuid == null)
            {
                return BadRequest(new Error("Token missing!"));
            }

            if (mathCalculation.FirstNumber == null || mathCalculation.SecondNumber == null || mathCalculation.Operation == 0) {
                return BadRequest(new Error("Math equation not complete!"));
            }

            decimal? Result = 0;

            try
            {
                mathCalculation = MathCalculation.Create(mathCalculation.FirstNumber, mathCalculation.SecondNumber, mathCalculation.Operation, Result, mathCalculation.FirebaseUuid);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


            switch (mathCalculation.Operation)
            {
                case 1:
                    mathCalculation.Result = mathCalculation.FirstNumber + mathCalculation.SecondNumber;
                    break;
                case 2:
                    mathCalculation.Result = mathCalculation.FirstNumber - mathCalculation.SecondNumber;
                    break;
                case 3:
                    mathCalculation.Result = mathCalculation.FirstNumber * mathCalculation.SecondNumber;
                    break;
                default:
                    mathCalculation.Result = mathCalculation.FirstNumber / mathCalculation.SecondNumber;
                    break;
            }

            if (ModelState.IsValid)
            {
                _context.Add(mathCalculation);
                await _context.SaveChangesAsync();
            }
            
            return Created(mathCalculation.CalculationId.ToString(), mathCalculation);
        }


        /// <summary>
        /// Gets the MathCalculation history for a user
        /// </summary>
        /// <param name="Token">Token of the current user.</param>
        /// <returns>A list of MathCalcuation objects</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /GetHistory
        ///     {
        ///        "Token": "{Insert token here}"
        ///     }
        /// </remarks>
        /// <response code="200">Returns the list of calculations for a user</response>
        /// <response code="400">Returns if a request is missing details or fails</response>
        /// <response code="404">Returns if no history found</response>

        [HttpGet("GetHistory")]
        [ProducesResponseType(typeof(List<MathCalculation>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public async Task<IActionResult> GetHistory(string Token)
        {
            if (Token == null)
            {
                return BadRequest(new Error("Token missing!"));
            }

            List<MathCalculation> historyItems = await _context.MathCalculations.Where(m => m.FirebaseUuid.Equals(Token)).ToListAsync();

            if (historyItems.Count > 0)
            {
                return Ok(historyItems);
            } else
            {
                return NotFound(new Error("No history found!"));
            }
        }


        /// <summary>
        /// Deletes the MathCalculation history for a user
        /// </summary>
        /// <param name="Token">Token of the current user.</param>
        /// <returns>List of deleted items</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /DeleteHistory
        ///     {
        ///        "Token": "{Insert token here}"
        ///     }
        /// </remarks>
        /// <response code="200">Returns the list of calculations deleted for a user</response>
        /// <response code="400">Returns if a request is missing details or fails</response>
        /// <response code="404">Returns if no history found</response>
        /// 
        [HttpDelete("DeleteHistory")]
        [ProducesResponseType(typeof(List<MathCalculation>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteHistory(string Token)
        {            
            if (Token == null)
            {
                return BadRequest(new Error("Token missing!"));
            }

            List<MathCalculation> removableItems = await _context.MathCalculations.Where(m => m.FirebaseUuid.Equals(Token)).ToListAsync();

            if (removableItems.Count > 0)
            {
                _context.MathCalculations.RemoveRange(removableItems);
                await _context.SaveChangesAsync();
                return Ok(removableItems);
            }
            else
            {
                return NotFound(new Error("No history found!"));
            }
        }
        
    }
}
