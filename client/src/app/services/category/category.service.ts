import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  private url: string = 'https://localhost:7125/api/CategoryConroller';

  constructor(
    private http: HttpClient
  ) { }

  
  getAllCategories(): any {
    return this.http.get<any>(this.url)
  }
  
  getCategory(id: string) {
    const data: string = JSON.stringify(id);
    return this.http.get<any>(this.url + '/' + id,
      {
        params: new HttpParams()
          .set('data', data)
      }
    )
  }

  createCategory(category: any) {
    const data: string = JSON.stringify(category);
    return this.http.post<any>(this.url, data);
  }

  // updateRequest(id: string, product: any) {
  //   const data: string = JSON.stringify(product);
  //   return this.http.put<any>(this.url + '/approve-request/' + id, data,
  //     {
  //       params: new HttpParams()
  //     }
  //   )
  // }






}
