interface MoveStrategy {
  move(): void;
}

class Walk implements MoveStrategy {
  move(): void {
    console.log("Waking...");
  }
}

class Fly implements MoveStrategy {
  move(): void {
    console.log("Flying");
  }
}

class Swim implements MoveStrategy {
  move(): void {
    console.log("Swimming");
  }
}

class Character {
  private strategy: MoveStrategy;

  constructor(strategy: MoveStrategy) {
    this.strategy = strategy;
  }

  setStrategy(strategy: MoveStrategy) {
    this.strategy = strategy;
  }

  move() {
    this.strategy.move();
  }
}

const character = new Character(new Walk());
character.move();
character.setStrategy(new Fly());
character.move();
character.setStrategy(new Swim());
character.move();
