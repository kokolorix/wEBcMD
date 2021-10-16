import { Guid } from "guid-typescript";

export function asGuid(guid: Guid|string): Guid
{
	if(guid instanceof Guid) return guid;

	return Guid.parse(guid);
}
export function equalsGuid(left: Guid | string | null | undefined, right: Guid | string | null | undefined): boolean {
   if(left === right)
   return true;

try {
   return asGuid(left).equals(asGuid(right));
} catch (error) {
   return false;
}}
