using Microsoft.VisualStudio.TestTools.UnitTesting;
using wEBcMD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wEBcMD.Tests
{
    record ValueDTO
    {
        public String Str
        {
            get
            {
                switch (this)
                {
                    case NumberValueDTO.IntValueDTO:
                        return ((NumberValueDTO.IntValueDTO)this).i.ToString();

                    default:
                        return String.Empty;
                }
            }
        }

        public record NumberValueDTO : ValueDTO
        {
			public NumberValueDTO() { }
			public NumberValueDTO(Int64 i): this(){ }

            public record IntValueDTO(Int64 i) : NumberValueDTO;
			public record FloatValueDTO(Double i) : NumberValueDTO;
        };
		public record BooleanValueDTO(Boolean b) : ValueDTO();
		public record StringValueDTO(String s) : ValueDTO();
		public record ObjectValueDTO(BaseDTO o) : ValueDTO();

    }
    //class NumberValueDTO : ValueDTO { }
    //class BooleanValueDTO : ValueDTO { }
    //class StringValueDTO : ValueDTO { }
    //class ObjectValueDTO : ValueDTO { }

	class ArgDTO
    {
		public String Name { get; set; }	

		public ValueDTO Value { get; set; }

    }

    [TestClass()]
	public class SetAdressTests
	{
		[TestMethod()]
		public void ExecuteCommandTest()
		{
			var v = new ValueDTO.NumberValueDTO.IntValueDTO(2);
			ArgDTO a = new() { Name = "Arg1" , Value = v };

			String s = a.Value.Str;

			CommandDTO cmd2;
			//{
			SetAdressWrapper setAdress = new();
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

			SetAdressWrapper check1 = new(), check2 = new(cmd2);

			check1.Result = setAdress.SetAdress(Guid.Empty, adress);

			//if (setAdress.Result.Id == Guid.Empty)
			//	Assert.Fail();
			//}
			//Assert.AreNotEqual(check1.Result, check2.Result);
		}
	}
}