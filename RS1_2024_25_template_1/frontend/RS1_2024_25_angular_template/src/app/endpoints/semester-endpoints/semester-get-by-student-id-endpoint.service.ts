import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {MyConfig} from '../../my-config';

export interface SemesterGetByStudentIdResponse {
  id:number;
  studentId:number;
  recordedBy:string;
  date:string;
  renewal:boolean;
  tuition:number,
  academicYear:string,
  yearOfStudy:number,
}

@Injectable({
  providedIn: 'root'
})
export class SemesterGetByStudentIdEndpointService  {
  private apiUrl = `${MyConfig.api_address}/semesters`;

  constructor(private httpClient: HttpClient) {
  }

  handleAsync(id: number) {
    return this.httpClient.get<SemesterGetByStudentIdResponse[]>(`${this.apiUrl}/${id}`);
  }
}
