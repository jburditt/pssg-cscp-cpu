import { iPerson } from "../person.interface";
import { iDynamicsPostUsers } from "../dynamics-post";
import { iDynamicsCrmContact } from "../dynamics-blob";
import { convertPersonToDynamics } from "./person-to-dynamics";

//this is a mapper function for converting people into dynamics users
export function convertPersonnelToDynamics(userId: string, organizationId: string, people: iPerson[]): iDynamicsPostUsers {
  const ppl: iDynamicsCrmContact[] = [];
  for (let person of people) {
    // convert the person to a contact
    const p: iDynamicsCrmContact = convertPersonToDynamics(person);
    // add person to the collection
    ppl.push(p);
  }
  return {
    UserBCeID: userId,
    BusinessBCeID: organizationId,
    StaffCollection: ppl
  } as iDynamicsPostUsers;
}
