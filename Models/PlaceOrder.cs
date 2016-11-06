using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BlowOut.Models
{
	public class PlaceOrder
	{
    	public Customers customer { get; set; }
        public Orders order { get; set; }

	}
}