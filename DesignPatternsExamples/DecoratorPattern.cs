using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DesignPatternsExamples
{
    public class DecoratorPattern
    {
    }

    /// <summary>
    /// Base abstract class
    /// </summary>
    public abstract class Pizza
    {
        public abstract double GetPrice();
        public abstract List<string> GetIngrendients();
    }

    /// <summary>
    /// Base implemantation class
    /// </summary>
    public class ThinCrust : Pizza
    {
        private new double _price = 200;

        public override double GetPrice()
        {
            return _price;
        }

        public override List<string> GetIngrendients()
        {
            return new List<string>
            {
                "wheat"
            };
        }
    }

    /// <summary>
    /// Base implemantation class
    /// </summary>
    public class ThikCrust : Pizza
    {
        private new double _price = 250;

        public override double GetPrice()
        {
            return _price;
        }
        public override List<string> GetIngrendients()
        {
            return new List<string>
            {
                "wheat", "extra wheat"
            };
        }
    }

    /// <summary>
    /// Decorator class, receive on constructor the base class
    /// </summary>
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

        public override List<string> GetIngrendients()
        {
            return basePizza.GetIngrendients();
        }
    }

    /// <summary>
    /// Decorator 
    /// </summary>
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

        public override List<string> GetIngrendients()
        {
            var ingrendients = basePizza.GetIngrendients();
            ingrendients.Add("onion");

            return ingrendients;
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
        public override List<string> GetIngrendients()
        {
            var ingrendients = basePizza.GetIngrendients();
            ingrendients.Add("cheese");

            return ingrendients;
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

        [Fact]
        public void DecoratorPatter_GetIngredient_thinCrust()
        {
            ThinCrust thinCrust = new ThinCrust();

            var response = thinCrust.GetIngrendients();

            Assert.Single(response);
            Assert.Contains("wheat", response);
        }

        [Fact]
        public void DecoratorPatter_GetIngredient_thikCrust()
        {
            ThikCrust thikCrust = new ThikCrust();

            var response = thikCrust.GetIngrendients();

            Assert.Equal(2,response.Count);
            Assert.Contains("wheat", response);
            Assert.Contains("extra wheat", response);
        }

        [Fact]
        public void DecoratorPatter_GetIngredient_thikCrust_Onion()
        {
            ThikCrust thikCrust = new ThikCrust();
            OnionPizzaDecorator onionPizzaDecorator = new OnionPizzaDecorator(thikCrust);

            var response = onionPizzaDecorator.GetIngrendients();

            Assert.Equal(3, response.Count);
            Assert.Contains("wheat", response);
            Assert.Contains("extra wheat", response);
            Assert.Contains("onion", response);
        }

        [Fact]
        public void DecoratorPatter_GetIngredient_thinCrust_Onion()
        {
            ThinCrust thinCrust = new ThinCrust();
            OnionPizzaDecorator onionPizzaDecorator = new OnionPizzaDecorator(thinCrust);

            var response = onionPizzaDecorator.GetIngrendients();

            Assert.Equal(2, response.Count);
            Assert.Contains("wheat", response);
            Assert.Contains("onion", response);
        }

        [Fact]
        public void DecoratorPatter_GetIngredient_thikCrust_Cheese()
        {
            ThikCrust thikCrust = new ThikCrust();
            CheesePizzaDecorator cheesePizzaDecorator = new CheesePizzaDecorator(thikCrust);

            var response = cheesePizzaDecorator.GetIngrendients();

            Assert.Equal(3, response.Count);
            Assert.Contains("wheat", response);
            Assert.Contains("extra wheat", response);
            Assert.Contains("cheese", response);
        }

        [Fact]
        public void DecoratorPatter_GetIngredient_thinCrust_Cheese()
        {
            ThinCrust thinCrust = new ThinCrust();
            CheesePizzaDecorator cheesePizzaDecorator = new CheesePizzaDecorator(thinCrust);

            var response = cheesePizzaDecorator.GetIngrendients();

            Assert.Equal(2, response.Count);
            Assert.Contains("wheat", response);
            Assert.Contains("cheese", response);
        }

        [Fact]
        public void DecoratorPatter_GetIngredient_thikCrust_Cheese_Onion()
        {
            ThikCrust thikCrust = new ThikCrust();
            CheesePizzaDecorator cheesePizzaDecorator = new CheesePizzaDecorator(thikCrust);
            OnionPizzaDecorator onionPizzaDecorator = new OnionPizzaDecorator(cheesePizzaDecorator);

            var response = onionPizzaDecorator.GetIngrendients();

            Assert.Equal(4, response.Count);
            Assert.Contains("wheat", response);
            Assert.Contains("extra wheat", response);
            Assert.Contains("cheese", response);
            Assert.Contains("onion", response);
        }

        [Fact]
        public void DecoratorPatter_GetIngredient_thinCrust_Cheese_Onion()
        {
            ThinCrust thinCrust = new ThinCrust();
            CheesePizzaDecorator cheesePizzaDecorator = new CheesePizzaDecorator(thinCrust);
            OnionPizzaDecorator onionPizzaDecorator = new OnionPizzaDecorator(cheesePizzaDecorator);

            var response = onionPizzaDecorator.GetIngrendients();

            Assert.Equal(3, response.Count);
            Assert.Contains("wheat", response);
            Assert.Contains("cheese", response);
            Assert.Contains("onion", response);
        }
    }
}
