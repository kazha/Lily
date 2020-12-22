import { Component, Inject } from '@angular/core';
import { Client, EmployeeModel } from 'src/app/models/Services';
import { Sort } from '@angular/material/sort';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {

  page = 0;
  pageSize = 5;
  employees: EmployeeModel[];
  sortedData: EmployeeModel[];

  constructor(private serviceClient: Client) {
   
  }

  ngOnInit() {
    this.serviceClient.employee().subscribe(data => {
      this.employees = data;
      this.sortedData = this.getEmployees();
    });
  }

  sortData(sort: Sort) {
    const data = this.getEmployees();
    if (!sort.active || sort.direction === '') {
      this.sortedData = data;
      return;
    }

    this.sortedData = data.sort((a, b) => {
      const isAsc = sort.direction === 'asc';
      switch (sort.active) {
        case 'firstName': return compare(a.firstName, b.firstName, isAsc);
        default: return 0;
      }
    })
  }

  filter(filterValue) {
    this.sortedData = this.getEmployees().filter(employee => {
      return filterValue === null || employee.firstName.includes(filterValue) || employee.lastName.includes(filterValue);
    });
  }

  getEmployees() {
    return this.employees;//.slice(this.page, this.pageSize);
  }

}

function compare(a: number | string, b: number | string, isAsc: boolean) {
  return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
}
