import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BaseChartDirective } from 'ng2-charts';

@Component({
  selector: 'app-manager-dashboard',
  standalone: true,
  imports: [CommonModule, BaseChartDirective],
  templateUrl: './manager-dashboard.html',
  styleUrl: './manager-dashboard.scss'
})
export class ManagerDashboard {

  projectChartData = {
    labels: ['Active', 'Completed', 'On Hold'],
    datasets: [
      {
        data: [6, 3, 1]
      }
    ]
  };

  attendanceChartData = {
    labels: ['Present', 'Absent', 'Late'],
    datasets: [
      {
        data: [15, 2, 1]
      }
    ]
  };

}
