import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(private snackBar: MatSnackBar) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
      catchError((error: HttpErrorResponse) => {

        if (error.error instanceof ErrorEvent) {
          this.snackBar.open(error.error.message, 'Fechar');
        } else {
          const errors = error.error;

          //todo: tratar melhor o caso de mais de um erro
          for (let index = 0; index < errors.length; index++) {
            this.snackBar.open(errors[index].description, 'Fechar');
          }
        }

        return throwError(() => error);
      })
    );
  }
}