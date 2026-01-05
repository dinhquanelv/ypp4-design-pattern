interface Windown {
  render(): void;
}

class SimpleWindown implements Windown {
  render(): void {
    console.log("Simple render");
  }
}

abstract class WindownDecorator implements Windown {
  protected wrapped: Windown;

  constructor(wrapped: Windown) {
    this.wrapped = wrapped;
  }

  render(): void {
    this.wrapped.render();
  }
}

class BorderDecorator extends WindownDecorator {
  render() {
    super.render();
    console.log("Add border");
  }
}

class ScrollDecorator extends WindownDecorator {
  render() {
    super.render();
    console.log("Add scroll");
  }
}

let windown: Windown = new SimpleWindown();
windown = new BorderDecorator(windown);
windown = new ScrollDecorator(windown);
windown.render();
