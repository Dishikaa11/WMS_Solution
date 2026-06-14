export interface Leave {

  leaveId: number;

  empId: number;

  leaveType: string;

  reason: string;

  fromDate: string;

  toDate: string;

  status: string;

  appliedOn: string;

  approvedBy?: number;

  approvedOn?: string;

}
