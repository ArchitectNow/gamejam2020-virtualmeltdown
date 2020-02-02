import { monitor } from '@colyseus/monitor';
import { Logger } from '@nestjs/common';
import { NestFactory } from '@nestjs/core';
import { Server } from 'colyseus';
import compression from 'compression';
import helmet from 'helmet';
import cors from 'cors';
import { AppModule } from './app.module';
import { GameRoom } from './game.room';

async function bootstrap() {
  const app = await NestFactory.create(AppModule);
  const logger = new Logger('NestApplication');
  app.use(cors());
  app.use(helmet());
  app.use(compression());

  const gameServer = new Server({ server: app.getHttpServer(), pingInterval: 0 });

  gameServer.define('game', GameRoom);

  app.use('/colyseus', monitor());

  logger.log('process port', process.env.PORT);
  logger.log('process node app instance', process.env.NODE_APP_INSTANCE);
  let port = Number(process.env.PORT) + Number(process.env.NODE_APP_INSTANCE);

  port = port || 3000;
  logger.log(port);

  await gameServer.listen(port, undefined, undefined, () => {
    logger.debug(`Server is running on ${port}`);
  });
}

bootstrap();
