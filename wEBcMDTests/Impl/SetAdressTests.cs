using Microsoft.VisualStudio.TestTools.UnitTesting;
using wEBcMD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wEBcMD.Tests
{
	[TestClass()]
	public class SetAdressTests
	{
		[TestMethod()]
		public void ExecuteCommandTest()
		{
			CommandDTO cmd1, cmd2;
			//{
			SetAdress setAdress = new();
			cmd2 = setAdress.Cmd;
			AdressDTO adress = new()
			{
				Name1 = "Name1",
				Name2 = "Name2",
				Adress1 = "Adress1",
				Housenumber = "42",
				Postcode = "3456",
				City = "City"
			};
			setAdress.Adress = adress;
			cmd1 = setAdress.ExecuteCommand();
			if (setAdress.Result.Id == Guid.Empty)
				Assert.Fail();
			//}
			SetAdress check1 = new(cmd1), check2 = new(cmd2);
			Assert.AreNotEqual(check1.Result, check2.Result);
		}
	}
}