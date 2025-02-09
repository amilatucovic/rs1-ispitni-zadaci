import {Component, OnInit} from '@angular/core';
import {MatTableDataSource} from '@angular/material/table';
import {ActivatedRoute, Router} from '@angular/router';
import {
  SemesterGetByStudentIdEndpointService,
  SemesterGetByStudentIdResponse
} from '../../../../endpoints/semester-endpoints/semester-get-by-student-id-endpoint.service';
import {
  StudentGetByIdEndpointService
} from '../../../../endpoints/student-endpoints/student-get-by-id-endpoint.service';
import {CityGetAll1Response} from '../../../../endpoints/city-endpoints/city-get-all1-endpoint.service';

@Component({
  selector: 'app-student-semesters',
  standalone: false,

  templateUrl: './student-semesters.component.html',
  styleUrl: './student-semesters.component.css'
})
export class StudentSemestersComponent implements OnInit {
  displayedColumns: string[] = ['id', 'academicYear', 'yearOfStudy', 'renewal', 'date', 'recordedBy'];
  dataSource: MatTableDataSource<SemesterGetByStudentIdResponse> = new MatTableDataSource<SemesterGetByStudentIdResponse>();
  studentId:number=0;
  semesters: SemesterGetByStudentIdResponse[] = [];
  studentFirstName: string='';
  studentLastName: string='';



  constructor(
    private semesterGetByStudentIdService: SemesterGetByStudentIdEndpointService,
    private router: Router,
    private studentGetByIdService: StudentGetByIdEndpointService,
    private route: ActivatedRoute,
  ) {
  }

  ngOnInit(): void {
    this.studentId = Number(this.route.snapshot.paramMap.get('id'));
    this.studentGetByIdService.handleAsync(this.studentId).subscribe({
      next: (data)=>{
        this.studentFirstName = data.firstName;
        this.studentLastName = data.lastName;
      },
      error: (err)=>
        console.error('Error fetching student data', err)
    });
    this.fetchSemesters(this.studentId);
  }

  fetchSemesters(studentId:number): void {
    this.semesterGetByStudentIdService.handleAsync(studentId).subscribe({
      next: (data) => {
        this.semesters = data;
        this.dataSource = new MatTableDataSource<SemesterGetByStudentIdResponse>(this.semesters);
        console.log(this.semesters);
      },
      error: (err) => console.error('Error fetching semesters:', err)
    });
  }

  enroll(studentId: number) {
    this.studentId = Number(this.route.snapshot.paramMap.get('id'));
    this.router.navigate(['/admin/student-semesters-new',studentId]);
  }
}
