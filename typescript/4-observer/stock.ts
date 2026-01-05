interface Observer {
  update(price: number): void;
}

interface Subject {
  attach(observer: Observer): void;
  detach(observer: Observer): void;
  notify(): void;
}

class Stock implements Subject {
  private observers: Observer[] = [];
  private price: number = 0;

  attach(observer: Observer) {
    this.observers.push(observer);
  }

  detach(observer: Observer) {
    this.observers = this.observers.filter((obs) => obs !== observer);
  }

  notify() {
    this.observers.forEach((obs) => obs.update(this.price));
  }

  setPrice(price: number) {
    this.price = price;
    this.notify();
  }
}

class Investor implements Observer {
  update(price: number) {
    console.log(`### New price ### ${price}`);
  }
}

const stock = new Stock();
const investor1 = new Investor();
const investor2 = new Investor();
stock.attach(investor1);
stock.attach(investor2);
stock.setPrice(100);
