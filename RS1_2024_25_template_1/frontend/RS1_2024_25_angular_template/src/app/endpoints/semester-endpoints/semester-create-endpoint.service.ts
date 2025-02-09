import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {MyConfig} from '../../my-config';

export interface SemesterCreateRequest {
  studentId: number;
  date: string;
  yearOfStudy:number,
  academicYearId:number,
  recordedById:number|undefined,
}

@Injectable({
  providedIn: 'root'
})
export class SemesterCreateEndpointService  {
  private apiUrl = `${MyConfig.api_address}/semesters`;

  constructor(private httpClient: HttpClient) {
  }

  handleAsync(request: SemesterCreateRequest) {
    return this.httpClient.post(`${this.apiUrl}`, request);
  }
}
