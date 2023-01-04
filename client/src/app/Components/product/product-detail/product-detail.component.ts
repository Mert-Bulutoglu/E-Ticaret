import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MessageLevel } from 'src/app/enums/message-level';
import { ApiResponse } from 'src/app/models/api-response';
import { Category } from 'src/app/models/category';
import { Product } from 'src/app/models/product';
import { CategoryService } from 'src/app/services/category/category.service';
import { ProductService } from 'src/app/services/product.service';
import { StateService } from 'src/app/services/state/state.service';
import { UtilsService } from 'src/app/services/utils/utils.service';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.scss']
})
export class ProductDetailComponent implements OnInit {
  form: FormGroup;
  categoriesList: Category;
  product: Product;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private productService: ProductService,
    private categoryService: CategoryService,
    private stateService: StateService,
    private utils: UtilsService,
  ) { }

  ngOnInit(): void {
    const productId: string = this.route.snapshot.paramMap.get('id');

    this.getAllCategories();

    if (productId) {
      this.getProduct(productId);
    }
    

    this.form = this.fb.group({
      productName: ['', [Validators.required, Validators.minLength(3)]],
      description: ['', [Validators.required, Validators.minLength(3)]],
      img1: ['', [Validators.required]],
      img2: ['', [Validators.required]],
      img3: ['', [Validators.required]],
      price: [''],
      stock: [''],
      categories: ['', [Validators.required]]
    });
  }

  getProduct(productId: string){
    this.stateService.showLoadingScreen();
    this.productService.getProduct(productId)
    .subscribe({
      next: (resp: ApiResponse) => {
        console.log(resp);
        this.product = resp.data;
        this.prefillForm(); 
      },
      error: (err: any) =>{
        this.stateService.hideLoadingScreen();
        this.stateService.showAppMessage(MessageLevel.error, `Something went wrong, please try again later.`)
        console.log(err);
      },
      complete: () => {
        this.stateService.hideLoadingScreen();
        console.log('get by id completed');
      } 
    })
  }

  getAllCategories() {
    this.stateService.showLoadingScreen();
    this.categoryService.getAllCategories()
      .subscribe({
        next: (resp: ApiResponse) => {
          console.log(resp);
          this.categoriesList = resp.data;
          if(this.product) {
            this.prefillForm();
          }
        },
        error: (err: any) => {
          this.stateService.hideLoadingScreen();
          this.stateService.showAppMessage(MessageLevel.error, `Something went wrong, please try again later.`)
          console.log(err);
        },
        complete: () => {
          this.stateService.hideLoadingScreen();
          console.log('get categories completed');
        }
      });
  }

  updateProduct(id: string, form: FormGroup){
    if(form.valid){
      this.stateService.showLoadingScreen('Updating user, please wait...');
      const product: Product = form.value;
      product.id = id;
      console.log(product);
      this.productService.updateProduct(id, product)
      .subscribe({
        next: (resp: ApiResponse) => {
          console.log(resp);
          this.stateService.showAppMessage(MessageLevel.success, `The product '${resp.data.productName}' was updated successfully.`);
        },
        error: (err: any) =>{
          this.stateService.hideLoadingScreen();
          this.stateService.showAppMessage(MessageLevel.error, `Something went wrong, please try again later.`)
          console.log(err);
        },
        complete: () => {
          this.stateService.hideLoadingScreen();
          console.log('product update completed');
        } 
      })
    } 
  }

  saveProduct(form: FormGroup) {
    if (form.valid) {
      this.stateService.showLoadingScreen('Saving product, please wait...')
      let product: Product = form.value;
      this.productService.createProduct(product)
        .subscribe({
          next: (resp: ApiResponse) => {
            console.log(resp);
            this.stateService.showAppMessage(MessageLevel.success, `The product '${resp.data.name}' was created successfully.`);
            this.router.navigate(['/product']);
          },
          error: (err: any) => {
            this.stateService.hideLoadingScreen();
            this.stateService.showAppMessage(MessageLevel.error, `Something went wrong, please try again later.`)
            console.log(err);
          },
          complete: () => {
            this.stateService.hideLoadingScreen();
            console.log('product add completed');
          }
        })
    }
  }

  handleSaveButton(form: FormGroup) {
    console.log(form.value);
    if (this.product) {
       this.updateProduct(this.product.id, form);
    } else {
      this.saveProduct(form);
    }
  }

  prefillForm(): void {
    this.productName.setValue(this.product.productName);
    this.description.setValue(this.product.description);
    this.img1.setValue(this.product.img1);
    this.img2.setValue(this.product.img2);
    this.img3.setValue(this.product.img3);
    this.stock.setValue(this.product.stock);
    this.price.setValue(this.product.price);
    this.categories.setValue(this.product.category)
  }


  get productName(): any {
    return this.form.get('productName');
  }

  get description(): any {
    return this.form.get('description');
  }

  get img1(): any {
    return this.form.get('img1');
  }

  get img2(): any {
    return this.form.get('img2');
  }

  get img3(): any {
    return this.form.get('img3');
  }

  get price(): any {
    return this.form.get('price');
  }

  get stock(): any {
    return this.form.get('stock');
  }

  get categories(): any {
    return this.form.get('categories');
  }

}
