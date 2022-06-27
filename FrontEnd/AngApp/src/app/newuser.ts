import {User} from './user';

export const NewUser: User = {
  id:0, name: "", surname: "",
  /* birthDate: new Date(1900,0,1),  */
  birthDate: '01/01/1900',
  userTypeId: 1, userTitleId: 1,
  emailAddress: "", isActive: true, userTitleDesc: "Employee",
  userTypeDesc: "User"
}

