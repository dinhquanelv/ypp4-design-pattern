interface SortStrategy {
  sort(data: number[]): void;
}

class BubbleSort implements SortStrategy {
  sort(data: number[]): number[] {
    return data.sort((a, b) => a - b); // example
  }
}

class QuickSort implements SortStrategy {
  sort(data: number[]): number[] {
    return data.sort((a, b) => b - a); // example
  }
}

class Sorter {
  private strategy: SortStrategy;

  constructor(strategy: SortStrategy) {
    this.strategy = strategy;
  }

  setStrategy(strategy: SortStrategy) {
    this.strategy = strategy;
  }

  sort(data: number[]) {
    const result = this.strategy.sort(data);
    console.log("Result", result);
  }
}

const sorter = new Sorter(new BubbleSort());
sorter.sort([3, 7, 1]);
sorter.setStrategy(new QuickSort());
sorter.sort([3, 7, 1]);
