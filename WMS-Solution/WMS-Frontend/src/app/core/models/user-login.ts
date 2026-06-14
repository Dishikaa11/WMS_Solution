export interface UserLogin {

  userId: number;

  username: string;

  passwordHash: string;

  employeeId: number;

  roleId: number;

  lastLogin?: string;

}