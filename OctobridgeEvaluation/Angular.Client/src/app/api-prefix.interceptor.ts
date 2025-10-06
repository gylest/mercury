import { HttpInterceptorFn } from '@angular/common/http';

export const apiPrefixInterceptor: HttpInterceptorFn = (req, next) => {
  const apiUrl = 'https://localhost:7273/';
  const url = req.url.startsWith('http') ? req.url : apiUrl + req.url.replace(/^\/+/, '');
  const apiReq = req.clone({ url });
  console.log('apiPrefixInterceptor called for URL:', req.url);
  return next(apiReq);
};
