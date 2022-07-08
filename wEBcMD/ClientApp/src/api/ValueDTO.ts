import { Guid } from "guid-typescript";

/** BaseDTO */
export class ValueDTO {

	/** 9b31d967-533c-4609-8dbb-f071a71e9b63is the Id of ValueDTO type. */
	static get TypeId(): Guid { return Guid.parse("9b31d967-533c-4609-8dbb-f071a71e9b63"); }

	Int32Value?: number;
	Int64Value?: number;
	DoubleValue?: number;
	BooleanValue?: boolean;
	StringValue?: string;
	ObjectType?: string;
	ObjectValue?: object;
};
