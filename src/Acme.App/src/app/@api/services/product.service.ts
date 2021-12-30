import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Product } from '@api';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { BASE_URL } from '@core';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(
    @Inject(BASE_URL) private readonly _baseUrl: string,
    private readonly _client: HttpClient
  ) { }

  public get(): Observable<Product[]> {
    return this._client.get<{ products: Product[] }>(`${this._baseUrl}api/product`)
      .pipe(
        map(x => x.products)
      );
  }

  public getById(options: { productId: string }): Observable<Product> {
    return this._client.get<{ product: Product }>(`${this._baseUrl}api/product/${options.productId}`)
      .pipe(
        map(x => x.product)
      );
  }

  public remove(options: { product: Product }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/product/${options.product.productId}`);
  }

  public create(options: { product: Product }): Observable<{ product: Product }> {
    return this._client.post<{ product: Product }>(`${this._baseUrl}api/product`, { product: options.product });
  }
  
  public update(options: { product: Product }): Observable<{ product: Product }> {
    return this._client.put<{ product: Product }>(`${this._baseUrl}api/product`, { product: options.product });
  }
}
