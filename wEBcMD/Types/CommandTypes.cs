using System;
using System.Collections.Generic;

namespace wEBcMD
{
   /// <summary>
   /// Base command object. Has all arguments in generig list, can be wrapped by concrete command wrappers
   /// </summary>
   public class CommandDTO : BaseDTO
   {
      /// <summary>c1eda1fc-cc45-4658-889f-ccd989c2848a is the Id of CommandDTO type.</summary>
      new public static Guid TypeId { get => System.Guid.Parse("c1eda1fc-cc45-4658-889f-ccd989c2848a"); }
      /// <summary>Indicates if this is the answer</summary>
      public virtual Boolean Response { get; set; } = false;
      /// <summary>Arguments of the command</summary>
      public virtual List<PropertyDTO> Arguments { get; set; } = new (){};
   };

   /// <summary>
   /// 
   /// This is the sample command. He has two Parameters
   /// and a multiline summary.
   /// ``` typescript
   /// CommandDTO cmd;
   /// if(SampleCommand.IsForMe(dto)){
   /// let sample = new SampleCommand(cmd);
   /// console.log(sample.FirstOne);
   /// }
   /// ```
   /// 
   /// </summary>
   public partial class SampleCommandWrapper : CommandWrapper
   {
      /// <summary>Constructor of SampleCommandWrapper</summary>
      public SampleCommandWrapper(CommandDTO dto = null):base(dto){}
      /// <summary>e3e185bd-5237-4574-977f-a040bbe12d35 is the Id of SampleCommandWrapper type.</summary>
      public static Guid TypeId { get => System.Guid.Parse("e3e185bd-5237-4574-977f-a040bbe12d35"); }
      /// <summary>Checks if the type of the DTO fits</summary>
      public static bool IsForMe(CommandDTO dto) => dto.Type == SampleCommandWrapper.TypeId;

		
		/// <summary>Create the wrapper and execute the command</summary>
		public static CommandDTO ExecuteCommand( CommandDTO dto )
		{
			SampleCommandWrapper wrapper = new(dto);
			
			wrapper.SampleCommand(
					wrapper.FirstOne, 
					wrapper.SecondOne
			);

			return wrapper.Cmd;
		}

		      /// <summary>
      /// 
      /// This is the sample command. He has two Parameters
      /// and a multiline summary.
      /// ``` typescript
      /// CommandDTO cmd;
      /// if(SampleCommand.IsForMe(dto)){
      /// let sample = new SampleCommand(cmd);
      /// console.log(sample.FirstOne);
      /// }
      /// ```
      /// 
      /// </summary>
		public partial void SampleCommand(String firstOne, Boolean secondOne);

		/// <summary>Serialize / Deserialize concrete SampleCommand to generic CommandDTO </summary>
		public override CommandDTO Cmd
		{
			get
			{
				CommandDTO cmd = base.Cmd;
				
				this.Set(cmd, "FirstOne", FirstOne);
				this.Set(cmd, "SecondOne", SecondOne);

				cmd.Response = true;
				return cmd;
			}
			set
			{
				CommandDTO cmd = value;
				
				this.Get(cmd, "FirstOne",  (()=>this.FirstOne, x => this.FirstOne	= x));
				this.Get(cmd, "SecondOne",  (()=>this.SecondOne, x => this.SecondOne	= x));
				
				base.Cmd = cmd;
				cmd.Response = false;
			}
		}
      /// <summary>
      /// 
      /// The FirstOne is a string parameter
      /// and has a multiline comment
      /// 
      /// </summary>
      public String FirstOne { get; set; }
      /// <summary>The SecondOne is a boolean parameter</summary>
      public Boolean SecondOne { get; set; }

   };


   static class CommandTypesDispatcher
   {
      public static CommandDTO Dispatch(CommandDTO dto)
      {
         if (null == dto)
            return dto;
         
         else if(SampleCommandWrapper.IsForMe(dto))
            return SampleCommandWrapper.ExecuteCommand(dto);
         
         return null;
      }
   }
}
