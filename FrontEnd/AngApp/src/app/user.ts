export interface User {
  id: number;
  name: string;
  surname: string;
  birthDate: Date;
  userTypeId: number;
  userTitleId: number;
  emailAddress: string;
  isActive: boolean;
}
