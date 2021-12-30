import { ChangeDetectionStrategy, Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Product, ProductService } from '@api';
import { combine } from '@core';
import { BehaviorSubject, Observable, of, Subject } from 'rxjs';
import { map, switchMap } from 'rxjs/operators';


@Component({
  selector: 'apm-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ProductsComponent {

  private readonly _saveSubject: Subject<Product> = new Subject();
  private readonly _selectedSubject: Subject<Product> = new Subject();
  private readonly _refreshSubject: BehaviorSubject<null> = new BehaviorSubject(null);

  readonly vm$ = this._refreshSubject
  .pipe(
    switchMap(_ => combine(
      this._productService.get(),
      this._activatedRoute
      .paramMap
      .pipe(
        map(x => x.get("productId")),
        switchMap(productId => productId ? this._productService.getById({ productId }) : of({ }))
        ),
        this._saveSubject.pipe(switchMap(product => this._handleSave(product))),
        this._selectedSubject.pipe(switchMap(product => this._handleSelect(product)))
    )),
    map(([products, selected]) => ({ products, selected }))
  );

  constructor(
    private readonly _activatedRoute: ActivatedRoute,
    private readonly _router: Router,
    private readonly _productService: ProductService
  ) { }

  private _handleSelect(product: Product): Promise<boolean> {
    const commands = product.productId 
    ? ["/","products","edit", product.productId]
    : ["/","products","create"];
    return this._router.navigate(commands);
  }

  private _handleSave(product: Product): Observable<boolean> {
    const obs$  = product.productId ? this._productService.update({ product }) : this._productService.create({ product });
    return obs$
    .pipe(
      switchMap(_ => this._router.navigate(["/","products"]))
      );    
  }

  onSave(product: Product) {
    this._saveSubject.next(product);
  }

  onSelect(product: Product) {
    this._selectedSubject.next(product);
  }
}
