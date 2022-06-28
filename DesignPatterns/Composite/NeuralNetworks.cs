using System.Collections;
using System.Collections.ObjectModel;

namespace Composite {
// Neuron and neuron layers example
// As a result, we are sure that even scalar values are treated as composite values or with singular content
    public class Neuron : IEnumerable<Neuron> {
        public float value;

        // There are incoming and outgoing neurons
        public List<Neuron> In, Out;

        // public void ConnectTo(Neuron other) {
        //     Out.Add(other);
        //     other.In.Add(this);
        // }

        // We're exposing a single neuron as IEnumerable and neuron layers are already IEnumerable
        // Therefore, now we can create an extension method! 
        public IEnumerator<Neuron> GetEnumerator() {
            yield return this;
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }

    public static class ExtensionMethods {
        public static void ConnectTo(this IEnumerable<Neuron> self, IEnumerable<Neuron> other) {
            // To check that neurons are not same
            if (ReferenceEquals(self, other)) return;

            foreach (var from in self) {
                foreach (var to in other) {
                    from.Out.Add(to);
                    to.In.Add(from);
                }
            }
        }
    }

    // Neurons can connect to other neurons, neurons can connect to neuron layers, and vice versa
    // In this case, just ConnectTo method is not enough, there's a big complexity
    // Thanks to composite pattern, one single method will be enough for it
    public class NeuronLayer : Collection<Neuron> { }

    public class Program {
        public static void main() {
            var neuron1 = new Neuron();
            var neuron2 = new Neuron();
            
            neuron1.ConnectTo(neuron2);

            var layer1 = new NeuronLayer();
            var layer2 = new NeuronLayer();
            
            neuron1.ConnectTo(layer1);
            layer2.ConnectTo(layer1);
        }
    }

}