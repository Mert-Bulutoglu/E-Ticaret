import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private url: string = 'https://localhost:7125/api/Products';

  constructor(
    private http: HttpClient
  ) { }


  getAllProducts(): any {
    return this.http.get<any>(this.url)
  }
  
  getProduct(id: string) {
    const data: string = JSON.stringify(id);
    return this.http.get<any>(this.url + '/' + id,
      {
        params: new HttpParams()
          .set('data', data)
      }
    )
  }

  createProduct(product: any) {
    const data: string = JSON.stringify(product);
    return this.http.post<any>(this.url, data);
  }

  updateRequest(id: string, product: any) {
    const data: string = JSON.stringify(product);
    return this.http.put<any>(this.url + '/approve-request/' + id, data,
      {
        params: new HttpParams()
      }
    )
  }



}
