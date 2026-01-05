interface Coffee {
  getCost(): number;
}

class SimpleCoffee implements Coffee {
  getCost(): number {
    return 20000;
  }
}

abstract class CoffeeDecorator implements Coffee {
  constructor(protected wrapped: Coffee) {}

  getCost(): number {
    return this.wrapped.getCost();
  }
}

class MilkDecorator extends CoffeeDecorator {
  getCost(): number {
    return super.getCost() + 5000;
  }
}

class CoconutDecorator extends CoffeeDecorator {
  getCost(): number {
    return super.getCost() + 10000;
  }
}

let coffee: Coffee = new SimpleCoffee();
coffee = new MilkDecorator(coffee);
coffee = new CoconutDecorator(coffee);
console.log(coffee.getCost());
