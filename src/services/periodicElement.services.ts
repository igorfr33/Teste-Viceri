import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PeriodicElement } from 'src/app/models/PeriodicElement';

@Injectable()
export class PeriodicElementService {
    elementApiGet = "https://localhost:7182/api/ListaHerois"
    elementApiPost = "https://localhost:7182/api/AdcionarHeroi";
    elementApiPut = "https://localhost:7182/api/AtualizaHeroi";
    elementApiDelete = "https://localhost:7182/api/DeletarHeroi";
  constructor(private http: HttpClient) {}

  getHeroi(): Observable<PeriodicElement[]>{
      return this.http.get<PeriodicElement[]>(this.elementApiGet);
  }

  createHeroi(element: PeriodicElement) : Observable<PeriodicElement>{
      return this.http.post<PeriodicElement>(this.elementApiPost, element);
  }

  editHeroi(element: PeriodicElement) : Observable<PeriodicElement>{
      return this.http.put<PeriodicElement>(this.elementApiPut, element);
  }

  deletHeroi(id: number) : Observable<any>{
      return this.http.delete<any>(`${this.elementApiDelete}?id=${id}`);
  }
}
