import { Guid} from "guid-typescript";
import { CommandWrapper } from "../impl/CommandWrapper";
import { CommandDTO } from "./CommandDTO";

/**
 * This is the sample command. He has two Parameters
 * and a multiline summary.
 * ``` typescript
 * CommandDTO cmd;
 * if(SampleCommand.IsForMe(dto)){
 *    let sample = new SampleCommand(cmd);
 *    console.log(sample.FirstOne);
 * }
 * ```
 */
export class SampleCommandAccess  extends CommandWrapper {

	constructor(dto?: CommandDTO, type?: Guid){super(dto, type ? type : SampleCommandAccess.TypeId)}

	/** e3e185bd-5237-4574-977f-a040bbe12d35 is the Id of SampleCommand type. */
	static get TypeId(): Guid { return Guid.parse("e3e185bd-5237-4574-977f-a040bbe12d35"); }

	/** Checks if the type of the DTO fits */
	static IsForMe(dto: CommandDTO) { return Guid.parse(dto.Type) === SampleCommandAccess.TypeId; }

	/**
	 * The FirstOne is a string parameter
	 * and has a multiline comment
	 */
	get FirstOne() : string{
		return this.getArgument("FirstOne");
	}
	set FirstOne( val : string) {
		this.setArgument("FirstOne", val);
	}

	/** The SecondOne is a boolean parameter */
	get SecondOne() : boolean{
		return Boolean(JSON.parse(this.getArgument("SecondOne")));
	}
	set SecondOne( val : boolean) {
		this.setArgument("SecondOne", val.toString());
	}


public execute(id:Guid):Promise<AdressDTO> {
      const headers = new HttpHeaders().set("Content-Type", 'application/json');
      this._http.post<;CommandDTO>;(
         this._baseUrl + 'Command/execute',
         sample.DTO
         , { headers, responseType: "json" }
      ).subscribe(result => {
         console.log(result);
      }, error => {
            console.error(error)
         });
}
         };