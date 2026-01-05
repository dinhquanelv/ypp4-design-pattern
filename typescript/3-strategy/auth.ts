interface AuthStrategy {
  signin(): void;
}

class LocalStrategy implements AuthStrategy {
  signin(): void {
    console.log("Sign in with Email");
  }
}

class GoogleStrategy implements AuthStrategy {
  signin(): void {
    console.log("Sign in with Google");
  }
}

class FacebookStrategy implements AuthStrategy {
  signin(): void {
    console.log("Sign in with Facebook");
  }
}

class Auth {
  constructor(private authStrategy: AuthStrategy) {}

  setStrategy(authStrategy: AuthStrategy) {
    this.authStrategy = authStrategy;
  }

  signin() {
    return this.authStrategy.signin();
  }
}

const userSignin = new Auth(new LocalStrategy());
userSignin.signin();
userSignin.setStrategy(new GoogleStrategy());
userSignin.signin();
userSignin.setStrategy(new FacebookStrategy());
userSignin.signin();
