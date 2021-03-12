using Xunit;

namespace DesignPatternsExamples
{
    public class DecoratorPattern
    {
    }

    public abstract class Pizza
    {
        public abstract double GetPrice();
    }

    public class ThikCrust : Pizza
    {
        private new double _price = 250;

        public override double GetPrice()
        {
            return _price;
        }
    }

    public class ThinCrust : Pizza
    {
        private new double _price = 200;

        public override double GetPrice()
        {
            return _price;
        }
    }

    public class Decorator : Pizza
    {
        readonly Pizza basePizza = null;

        protected double price = 0;

        protected Decorator(Pizza pizza)
        {
            basePizza = pizza;
        }
        public override double GetPrice()
        {
            return price + basePizza.GetPrice();
        }
    }

    public class OnionPizzaDecorator : Decorator
    {
        Pizza basePizza;
        public OnionPizzaDecorator(Pizza pizza) : base(pizza)
        {
            basePizza = pizza;
        }

        protected new double price = 100;

        public override double GetPrice()
        {
            return price + basePizza.GetPrice();
        }
    }

    public class CheesePizzaDecorator : Decorator
    {
        Pizza basePizza;
        public CheesePizzaDecorator(Pizza pizza) : base(pizza)
        {
            basePizza = pizza;
        }

        protected new double price = 100;

        public override double GetPrice()
        {
            return price + basePizza.GetPrice();
        }
    }

    public class TestDecoratorPattern
    {
        [Fact]
        public void DecoratorPattern_thinCrust()
        {
            ThinCrust thinCrust = new ThinCrust();
            OnionPizzaDecorator onionPizzaDecorator = new OnionPizzaDecorator(thinCrust);
            CheesePizzaDecorator cheesePizzaDecorator = new CheesePizzaDecorator(onionPizzaDecorator);

            var respThinCrust = thinCrust.GetPrice();
            var respOnionWithThinCrust = onionPizzaDecorator.GetPrice();
            var respCheeseWithOnionAndThinCrust = cheesePizzaDecorator.GetPrice();

            Assert.Equal(200, respThinCrust);
            Assert.Equal(300, respOnionWithThinCrust);
            Assert.Equal(400, respCheeseWithOnionAndThinCrust);
        }

        [Fact]
        public void DecoratorPattern_thikCrust()
        {
            ThikCrust thikCrust = new ThikCrust();
            OnionPizzaDecorator onionPizzaDecorator = new OnionPizzaDecorator(thikCrust);
            CheesePizzaDecorator cheesePizzaDecorator = new CheesePizzaDecorator(onionPizzaDecorator);

            var respThikCrust = thikCrust.GetPrice();
            var respOnionWithThinCrust = onionPizzaDecorator.GetPrice();
            var respCheeseWithOnionAndThinCrust = cheesePizzaDecorator.GetPrice();

            Assert.Equal(250, respThikCrust);
            Assert.Equal(350, respOnionWithThinCrust);
            Assert.Equal(450, respCheeseWithOnionAndThinCrust);
        }

        [Fact]
        public void DecoratorPattern_OnionWithCheeseThinCrust()
        {
            ThinCrust thinCrust = new ThinCrust();
            CheesePizzaDecorator cheesePizzaDecorator = new CheesePizzaDecorator(thinCrust);
            OnionPizzaDecorator onionPizzaDecorator = new OnionPizzaDecorator(cheesePizzaDecorator);

            var respThinCrust = thinCrust.GetPrice();
            var respCheeseWithThinCrust = cheesePizzaDecorator.GetPrice();
            var respOnionWithCheeseAndThinCrust = onionPizzaDecorator.GetPrice();

            Assert.Equal(200, respThinCrust);
            Assert.Equal(300, respCheeseWithThinCrust);
            Assert.Equal(400, respOnionWithCheeseAndThinCrust);
        }

        [Fact]
        public void DecoratorPattern_OnionWithCheeseThikCrust()
        {
            ThikCrust thikCrust = new ThikCrust();
            CheesePizzaDecorator cheesePizzaDecorator = new CheesePizzaDecorator(thikCrust);
            OnionPizzaDecorator onionPizzaDecorator = new OnionPizzaDecorator(cheesePizzaDecorator);

            var respThikCrust = thikCrust.GetPrice();
            var respCheeseWithThinCrust = cheesePizzaDecorator.GetPrice();
            var respOnionWithCheeseAndThinCrust = onionPizzaDecorator.GetPrice();

            Assert.Equal(250, respThikCrust);
            Assert.Equal(350, respCheeseWithThinCrust);
            Assert.Equal(450, respOnionWithCheeseAndThinCrust);
        }
    }
}
