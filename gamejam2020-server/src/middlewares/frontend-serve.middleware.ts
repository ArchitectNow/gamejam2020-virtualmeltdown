import { Injectable, NestMiddleware } from '@nestjs/common';
import { Request, Response } from 'express';
import { resolve } from 'path';

const allowedExt = ['.js', '.ico', '.css', '.png', '.jpg', '.woff2', '.woff', '.ttf', '.svg'];

const resolvePath = (file: string) => resolve(__dirname, '../../public', file);

@Injectable()
export class FrontendServeMiddleware implements NestMiddleware {
  use(req: Request, res: Response, next) {
    const { baseUrl } = req;
    if (baseUrl.indexOf('v1') === 1 || baseUrl.indexOf('matchmake') === 1) {
      next();
    } else if (allowedExt.filter(ext => baseUrl.indexOf(ext) > 0).length > 0) {
      res.sendFile(resolvePath(baseUrl));
    } else {
      res.sendFile(resolvePath('index.html'));
    }
  };
}
