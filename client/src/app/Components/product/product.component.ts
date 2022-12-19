import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { ApiResponse } from 'src/app/models/api-response';
import { Product } from 'src/app/models/product';
import { ProductService } from 'src/app/services/product.service';





@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss']
})
export class ProductComponent implements AfterViewInit {

  products: Product[] = [];
  productDataSource: MatTableDataSource<Product>;
  columnsToDisplay = ['productName', 'price', 'stock','category'];
  columnsToDisplayWithExpand = [...this.columnsToDisplay, 'expand'];


  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(
    private productService: ProductService,
    private router: Router,
  ) {
    this.productDataSource = new MatTableDataSource(this.products);
  }
 
  ngOnInit(): void {
    this.productService.getAllProducts().subscribe({
      next: (resp: ApiResponse) => {
        this.products = resp.data
        console.log(this.products);
        this.productDataSource = new MatTableDataSource(this.products);
        this.productDataSource.paginator = this.paginator;
        this.productDataSource.sort = this.sort;
      },
      error: (err: any) => {
        console.log(err);
      },
      complete: () => {
        console.log('user add completed');
      }
    })
  }

  ngAfterViewInit(): void {
    this.productDataSource.paginator = this.paginator;
    this.productDataSource.sort = this.sort;
  }


  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.productDataSource.filter = filterValue.trim().toLowerCase();

    if (this.productDataSource.paginator) {
      this.productDataSource.paginator.firstPage();
    }
  }
}

