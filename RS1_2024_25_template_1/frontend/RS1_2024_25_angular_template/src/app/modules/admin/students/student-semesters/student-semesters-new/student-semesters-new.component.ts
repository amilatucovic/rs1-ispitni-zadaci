import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {ActivatedRoute, Router} from '@angular/router';
import {
  AcademicYearGetAllEndpointService,
  AcademicYearGetAllResponse
} from '../../../../../endpoints/semester-endpoints/academic-year-get-all-endpoint-service';
import {
  SemesterCreateEndpointService, SemesterCreateRequest
} from '../../../../../endpoints/semester-endpoints/semester-create-endpoint.service';
import {
  StudentGetByIdEndpointService
} from '../../../../../endpoints/student-endpoints/student-get-by-id-endpoint.service';
import {
  SemesterGetByStudentIdEndpointService, SemesterGetByStudentIdResponse
} from '../../../../../endpoints/semester-endpoints/semester-get-by-student-id-endpoint.service';
import {MyAuthService} from '../../../../../services/auth-services/my-auth.service';
import {MySnackbarHelperService} from '../../../../shared/snackbars/my-snackbar-helper.service';

@Component({
  selector: 'app-student-semesters-new',
  standalone: false,

  templateUrl: './student-semesters-new.component.html',
  styleUrl: './student-semesters-new.component.css'
})
export class StudentSemestersNewComponent implements OnInit {
  semesterForm: FormGroup;
  studentId: number;
  semesters: SemesterGetByStudentIdResponse[] = [];
  academicYears: AcademicYearGetAllResponse[]=[];
  studentFirstName: string='';
  studentLastName: string='';


  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    public router: Router,
    private semesterCreateService: SemesterCreateEndpointService,
    private studentGetById: StudentGetByIdEndpointService,
    private semesterGetByStudentId: SemesterGetByStudentIdEndpointService,
    private academicYearsGetService: AcademicYearGetAllEndpointService,
    private myAuthInfo: MyAuthService,
    private snackbar: MySnackbarHelperService
  ) {
    this.studentId = 0;

    this.semesterForm = this.fb.group({
      date: [null, [Validators.required]],
      yearOfStudy: ['', [Validators.required, Validators.min(1), Validators.max(5)]],
      academicYearId: [null, [Validators.required]],
      tuition:[{value:0, disabled:true}],
      renewal:[{value:false, disabled:true}],
    });
  }

  ngOnInit(): void {
    this.studentId = Number(this.route.snapshot.paramMap.get('id'));
    this.semesterGetByStudentId.handleAsync(this.studentId).subscribe({next:(data) => {
      this.semesters = data;
      }});
    this.semesterForm.get('yearOfStudy')?.valueChanges.subscribe((yearOfStudy: number) => {
      const obnova=this.semesters.find(s=>s.yearOfStudy == yearOfStudy);
      if(obnova){
        this.semesterForm.get('tuition')?.setValue(400);
        this.semesterForm.get('renewal')?.setValue(true);
      }
      else{
        this.semesterForm.get('tuition')?.setValue(1800);
        this.semesterForm.get('renewal')?.setValue(false);
      }
    });
    if (this.studentId) {
      this.loadStudentData();
    }
    this.loadYears();

    this.semesterForm.get('countryId')?.valueChanges.subscribe((countryId: number) => {
    });
  }

  loadStudentData(): void {
    this.studentGetById.handleAsync(this.studentId).subscribe({
      next: (s) => {
       this.studentFirstName=s.firstName;
       this.studentLastName=s.lastName;
      },
      error: (error) => console.error('Error loading student data', error),
    });
  }

  loadYears(): void {
    this.academicYearsGetService.handleAsync().subscribe({
      next: (years) => (this.academicYears = years),
      error: (error) => console.error('Error loading academic years', error),
    });
  }


  createSemester() {
    let semesterRequest: SemesterCreateRequest = {
      studentId:Number(this.route.snapshot.paramMap.get('id')),
      date:this.semesterForm.get('date')?.value,
      yearOfStudy:this.semesterForm.get('yearOfStudy')?.value,
      academicYearId:this.semesterForm.get('academicYearId')?.value,
      recordedById:this.myAuthInfo.getMyAuthInfo()?.userId

    }
    this.semesterCreateService.handleAsync(semesterRequest).subscribe({next:() => {
        this.snackbar.showMessage('Semester successfully added.');
        this.router.navigate(['/admin/student-semesters',Number(this.route.snapshot.paramMap.get('id'))]);
      }})
  }
}
