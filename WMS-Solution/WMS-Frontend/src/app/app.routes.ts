import { Routes } from '@angular/router';
import { Login } from './features/auth/login/login';
import { Shell } from './layout/shell/shell';
import { Dashboard } from './features/dashboard/dashboard/dashboard';
import { EmployeeList } from './features/employees/employee-list/employee-list';
import { EmployeeForm } from './features/employees/employee-form/employee-form';
import { AttendanceList } from './features/attendance/attendance-list/attendance-list';
import { AttendanceForm } from './features/attendance/attendance-form/attendance-form';
import { LeaveList } from './features/leaves/leave-list/leave-list';
import { LeaveForm } from './features/leaves/leave-form/leave-form';
import { ProjectList } from './features/projects/project-list/project-list';
import { ProjectForm } from './features/projects/project-form/project-form';
import { ClientList } from './features/clients/client-list/client-list';
import { ClientForm } from './features/clients/client-form/client-form';
import { AnnouncementList } from './features/announcements/announcement-list/announcement-list';
import { AnnouncementForm } from './features/announcements/announcement-form/announcement-form';
import { EmployeeAllocationList } from './features/allocation/allocation-list/allocation-list'; 
import { EmployeeAllocationForm } from './features/allocation/allocation-form/allocation-form';
import { RoleList } from './features/role/role-list/role-list';
import { RoleForm } from './features/role/role-form/role-form';
import { UserLoginList } from './features/user-login/user-login-list/user-login-list';
import { UserLoginForm } from './features/user-login/user-login-form/user-login-form';
import { ManagerDashboard }from './features/dashboard/manager-dashboard/manager-dashboard';
import { EmployeeDashboard } from './features/dashboard/employee-dashboard/employee-dashboard';
import { ChangePassword } from './features/auth/change-password/change-password';
import { AuditLogList } from './features/audit-log/audit-log-list/audit-log-list';


export const routes: Routes = [
    
{
  path:'',
  redirectTo:'login',
  pathMatch:'full'
},
  {
    path: 'login',
    component: Login
  },


    {
    path:'',
    component: Shell,
    children:[
      {
        path:'dashboard',
        component: Dashboard
      },
      {
        path:'',
        redirectTo:'dashboard',
        pathMatch:'full'
      },
      {
        path:'employees',
        component:EmployeeList
    },
    
{
  path:'employees/add',
  component: EmployeeForm
},
{
  path:'employees/edit/:id',
  component: EmployeeForm
},
{
  path: 'departments',
  loadComponent: () =>
    import(
      './features/departments/department-list/department-list'
    ).then(
      m => m.DepartmentList
    )
},

{
  path: 'departments/add',
  loadComponent: () =>
    import(
      './features/departments/department-form/department-form'
    ).then(
      m => m.DepartmentForm
    )
},

{
  path: 'departments/edit/:id',
  loadComponent: () =>
    import(
      './features/departments/department-form/department-form'
    ).then(
      m => m.DepartmentForm
    )
},

      {
        path: 'attendance',
        component: AttendanceList
      },
      {
        path: 'attendance/add',
        component: AttendanceForm
      },
      {
        path: 'attendance/edit/:id',
        component: AttendanceForm
      },

      {
      path:'leaves',
      component: LeaveList
    },

    {
      path:'leaves/add',
      component: LeaveForm
    },

    {
      path:'leaves/edit/:id',
      component: LeaveForm
    },

    {
      path:'projects',
      component: ProjectList
    },

    {
      path:'projects/add',
      component: ProjectForm
    },

    {
      path:'projects/edit/:id',
      component: ProjectForm
    },

    {
      path:'clients',
      component: ClientList
    },

    {
      path:'clients/add',
      component: ClientForm
    },

    {
      path:'clients/edit/:id',
      component: ClientForm
    },

    {
      path:'announcements',
      component: AnnouncementList
    },

    {
      path:'announcements/add',
      component: AnnouncementForm
    },
    {
      path:'announcements/edit/:id',
      component: AnnouncementForm
    },

    {
      path:'allocations',
      component:
      EmployeeAllocationList
    },

    {
      path:'allocations/add',
      component:
      EmployeeAllocationForm
    },

    {
      path:'allocations/edit/:id',
      component:
      EmployeeAllocationForm
    },

    {
      path:'roles',
      component: RoleList
    },

    {
      path:'roles/add',
      component: RoleForm
    },

    {
      path:'roles/edit/:id',
      component: RoleForm
    },
    {
      path:'user-logins',
      component: UserLoginList
    },

    {
      path:'user-logins/add',
      component: UserLoginForm
    },

    {
      path:'user-logins/edit/:id',
      component: UserLoginForm
    },

    {
      path:'admin-dashboard',
      component: Dashboard
    },

    {
      path:'manager-dashboard',
      component: ManagerDashboard
    },

    {
      path:'employee-dashboard',
      component: EmployeeDashboard
    },
    {
      path:'change-password',
      component: ChangePassword
    },
    {
      path:'audit-logs',
      component: AuditLogList
    },

    {
      path:'my-leaves',
      component: LeaveList
    },
    {
      path:'my-attendance',
      component: AttendanceList
    }

    ]
  }

];
