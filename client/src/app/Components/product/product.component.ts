import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { MessageLevel } from 'src/app/enums/message-level';
import { ApiResponse } from 'src/app/models/api-response';
import { ConfirmationDialog } from 'src/app/models/confirmation-dialog';
import { Product } from 'src/app/models/product';
import { ProductService } from 'src/app/services/product.service';
import { StateService } from 'src/app/services/state/state.service';
import { ConfirmationDialogComponent } from '../shared/confirmation-dialog/confirmation-dialog.component';





@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss']
})
export class ProductComponent implements AfterViewInit {

  products: Product[] = [];
  productDataSource: MatTableDataSource<Product>;
  columnsToDisplay = ['productName', 'price', 'stock','category','action'];
  columnsToDisplayWithExpand = [...this.columnsToDisplay, 'expand'];
  selectedProduct: Product;



  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(
    private productService: ProductService,
    private router: Router,
    public dialogRef: MatDialog,
    private stateService: StateService

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


  openDeleteConfirmation(id: number | string) {
    const selectedProduct : Product = this.products.find(u => u.id === id);
    const data: ConfirmationDialog = {
      message: `Are you sure you want to remove the product '${selectedProduct.productName}'`,
      data: selectedProduct
    }
    const dialogRef = this.dialogRef.open(ConfirmationDialogComponent, {
      width: '500px',
      data: data
    });

    dialogRef.afterClosed().subscribe(result => {
      if( result.success ) this.deleteProduct(selectedProduct.id);
    });
  } 
  
  showProductEdit(id: number | string): void {
    this.router.navigate(['/product', id]) 
  }

  deleteProduct(id: number | string){
    const productIdex = this.products.findIndex(u => u.id === id);
    this.stateService.showAppMessage(MessageLevel.success, `The customer '${this.products[productIdex].productName}' was deleted successfully.`);
    if (productIdex > -1) { 
      this.products.splice(productIdex, 1); 
    }
    this.productDataSource = new MatTableDataSource(this.products);
    this.productDataSource.paginator = this.paginator;
    this.productDataSource.sort = this.sort;
  } 




}

