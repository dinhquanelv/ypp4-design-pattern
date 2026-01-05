interface Payment {
  pay(): void;
}

class MoMoPayment implements Payment {
  pay(): void {
    console.log("Pay with MoMo");
  }
}

class BankingPayment implements Payment {
  pay(): void {
    console.log("Pay with Banking");
  }
}

class VisaPayment implements Payment {
  pay(): void {
    console.log("Pay with Visa");
  }
}

class PaymentFactory {
  private static readonly payments = new Map<string, new () => Payment>([
    ["momo", MoMoPayment],
    ["banking", BankingPayment],
    ["visa", VisaPayment],
  ]);

  static createPayment(type: string) {
    const PaymentClass = this.payments.get(type);

    if (!PaymentClass) {
      throw new Error("Invalid type");
    }

    return new PaymentClass();
  }
}

const momo = PaymentFactory.createPayment("momo");
const banking = PaymentFactory.createPayment("banking");
const visa = PaymentFactory.createPayment("visa");

momo.pay();
banking.pay();
visa.pay();
