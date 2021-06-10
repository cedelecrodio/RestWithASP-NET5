using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithASPNET5.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Calculator : ControllerBase
    {
        private ILogger<Calculator> _logger;

        public Calculator(ILogger<Calculator> logger)
        {
            _logger = logger;
        }

        [HttpGet("calc/{firstNumber}/{Operator}/{secondNumber}")]
        public IActionResult Get(decimal logicalOperator, string firstNumber, string secondNumber, string Operator) //string firstNumber, string secondNumber, string Operator
        {
            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var result = WhichOperation(firstNumber, secondNumber, Operator, logicalOperator);
                return Ok(result);
            }
            return BadRequest("Invalid Input");
        }

        private string WhichOperation(string firstNumber, string secondNumber, string Operator, decimal logicalOperator)
        {
            //decimal logicalOperator; 
            switch (Operator)
            {
                case "sum":
                    logicalOperator = ConverTtoDecimal(firstNumber) + ConverTtoDecimal(secondNumber);
                    break;
                case "sub":
                    logicalOperator = ConverTtoDecimal(firstNumber) - ConverTtoDecimal(secondNumber);
                    break;
                case "mult":
                    logicalOperator = ConverTtoDecimal(firstNumber) * ConverTtoDecimal(secondNumber);
                    break;
                case "div":
                    logicalOperator = ConverTtoDecimal(firstNumber) / ConverTtoDecimal(secondNumber);
                    break;
                default:
                    return "Invalid Operator";
            }
            var r = ConverTtoDecimal(logicalOperator.ToString());
            return r.ToString();
        }



        // verifica se são entradas válidas (números)
        private bool IsNumeric(string strNumber)
        {
            double number;
            bool isNumber = double.TryParse(
                   strNumber
                 , System.Globalization.NumberStyles.Any
                 , System.Globalization.NumberFormatInfo.InvariantInfo
                 , out number);
            return isNumber;
        }

        // converte os números de string para decimal
        private decimal ConverTtoDecimal(string strNumber)
        {
            decimal decimalValue;
            if(decimal.TryParse(strNumber, out decimalValue))
            {
                return decimalValue;
            }
            return 0;
        }

    }
}
