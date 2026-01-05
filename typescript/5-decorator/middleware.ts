interface Middleware {
  use(): void;
}

class DefaultMiddleware implements Middleware {
  use(): void {
    console.log("Apply Default Middleware");
  }
}

abstract class MiddlewareDecorator implements Middleware {
  constructor(protected wrapped: Middleware) {}

  use(): void {
    this.wrapped.use();
  }
}

class LoggerDecorator extends MiddlewareDecorator {
  use(): void {
    super.use();
    console.log("Apply Logger Middleware");
  }
}

class CorsDecorator extends MiddlewareDecorator {
  use(): void {
    super.use();
    console.log("Apply Cors Middleware");
  }
}

let middleware: Middleware = new DefaultMiddleware();
middleware = new LoggerDecorator(middleware);
middleware = new CorsDecorator(middleware);
middleware.use();
