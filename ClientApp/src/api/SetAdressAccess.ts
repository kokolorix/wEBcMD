import { Guid} from "guid-typescript";
import { AdressDTO } from "./AdressDTO";
import { CommandWrapper } from "../impl/CommandWrapper";
import { CommandDTO } from "./CommandDTO";

/**
 */
export class SetAdressAccess  extends CommandWrapper {

	constructor(dto?: CommandDTO, type?: Guid){super(dto, type ? type : SetAdressAccess.TypeId)}

	/** c84bb99b-2d11-4426-87fa-119dc892f4ec is the Id of SetAdress type. */
	static get TypeId(): Guid { return Guid.parse("c84bb99b-2d11-4426-87fa-119dc892f4ec"); }

	/** Checks if the type of the DTO fits */
	static IsForMe(dto: CommandDTO) { return Guid.parse(dto.Type) === SetAdressAccess.TypeId; }

	/** Id */
	get Id() : Guid{
		return Guid.parse(this.getArgument("Id"));
	}
	set Id( val : Guid) {
		this.setArgument("Id", val.toString());
	}

	/**  */
	get Adress() : AdressDTO{
		return JSON.parse(this.getArgument("Adress")) as AdressDTO ;
	}
	set Adress( val : AdressDTO) {
		this.setArgument("Adress", JSON.stringify(val));
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