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
		public static ValueDTO Create(Int32 v) => new ValueDTO.NumberValueDTO.Int32ValueDTO(v);
		public static ValueDTO Create(Int64 v) => new ValueDTO.NumberValueDTO.Int64ValueDTO(v);
        public static ValueDTO Create(Double v) => new ValueDTO.NumberValueDTO.FloatValueDTO(v);
        public static ValueDTO Create(String v) => new ValueDTO.NumberValueDTO.StringValueDTO(v);
        public static ValueDTO Create(Boolean v) => new ValueDTO.NumberValueDTO.BooleanValueDTO(v);
        public static ValueDTO Create(BaseDTO v) => new ValueDTO.NumberValueDTO.ObjectValueDTO(v);

        public String Str
        {
            get
            {
                switch (this)
                {
                    case NumberValueDTO.Int32ValueDTO:
                        return ((NumberValueDTO.Int32ValueDTO)this).value.ToString();

                    case NumberValueDTO.Int64ValueDTO:
                        return ((NumberValueDTO.Int64ValueDTO)this).value.ToString();

                    case NumberValueDTO.FloatValueDTO:
                        return ((NumberValueDTO.FloatValueDTO)this).value.ToString();

                    case NumberValueDTO.BooleanValueDTO:
                        return ((NumberValueDTO.BooleanValueDTO)this).value.ToString();

                    case NumberValueDTO.StringValueDTO:
                        return ((NumberValueDTO.StringValueDTO)this).value.ToString();

                    case NumberValueDTO.ObjectValueDTO:
                        return ((NumberValueDTO.ObjectValueDTO)this).value.ToString();

                    default:
                        return String.Empty;
                }
            }
        }

        public record NumberValueDTO : ValueDTO
        {
			public NumberValueDTO() { }
			public NumberValueDTO(Int64 i): this(){ }

            public record Int32ValueDTO(Int32 value) : NumberValueDTO;
             public record Int64ValueDTO(Int64 value) : NumberValueDTO;
           public record FloatValueDTO(Double value) : NumberValueDTO;
        };
		public record BooleanValueDTO(Boolean value) : ValueDTO();
		public record StringValueDTO(String value) : ValueDTO();
		public record ObjectValueDTO(BaseDTO value) : ValueDTO();

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
			var v = ValueDTO.Create(2);
			ArgDTO a = new() { Name = "Arg1" , Value = v };

			String s = a.Value.Str;

            List<ArgDTO> args = new()
            {
                new (){ Name= "Arg1", Value = ValueDTO.Create(7) },
                new (){ Name= "Arg2", Value = ValueDTO.Create(3.14) },
                new (){ Name= "Arg3", Value = ValueDTO.Create(true) },
                new (){ Name= "Arg4", Value = ValueDTO.Create("Holdrio") },
                new (){ Name= "Arg1", Value = ValueDTO.Create(
                    new AdressDTO(){ Adress1 = "Street1", Name1= "Name1"}
                    ) },
            };

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