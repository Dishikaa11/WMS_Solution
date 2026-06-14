export interface EmployeeDashboard {

  name: string;

  email: string;

  department: string;

  role: string;

  doj: string;

  presentDays: number;

  absentDays: number;

  attendancePercentage: number;

  appliedLeaves: number;

  approvedLeaves: number;

  rejectedLeaves: number;

  pendingLeaves: number;

  recentAttendance: any[];

  recentLeaves: any[];

  projects: any[];

  announcements: any[];

}
