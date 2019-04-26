using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EbayService.Controllers.ResponseObjects
{
  public class Response
  {
    public bool IsSuccess { get; set; }
    public string ErrorMessage { get; set; }
  }
}
