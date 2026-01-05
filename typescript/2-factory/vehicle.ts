interface Vehicle {
  drive(): void;
}

class Car implements Vehicle {
  drive(): void {
    console.log("Drive Car");
  }
}

class Plane implements Vehicle {
  drive(): void {
    console.log("Drive Plane");
  }
}

class VehicleFactory {
  private static readonly vehicles = new Map<string, new () => Vehicle>([
    ["car", Car],
    ["plane", Plane],
  ]);

  static createVehicle(type: string): Vehicle {
    const VehicleClass = this.vehicles.get(type);

    if (!VehicleClass) {
      throw new Error("Invalid type");
    }

    return new VehicleClass();
  }
}

const car = VehicleFactory.createVehicle("car");
const plane = VehicleFactory.createVehicle("plane");

car.drive();
plane.drive();
