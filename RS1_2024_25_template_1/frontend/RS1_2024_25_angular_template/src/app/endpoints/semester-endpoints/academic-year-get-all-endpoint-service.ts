import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {MyConfig} from '../../my-config';

export interface AcademicYearGetAllResponse {
  id: number;
  description:string;
}

@Injectable({
  providedIn: 'root'
})
export class AcademicYearGetAllEndpointService   {
  private apiUrl = `${MyConfig.api_address}/academic-years`;

  constructor(private httpClient: HttpClient) {
  }

  handleAsync() {
    return this.httpClient.get<AcademicYearGetAllResponse[]>(`${this.apiUrl}`);
  }
}
