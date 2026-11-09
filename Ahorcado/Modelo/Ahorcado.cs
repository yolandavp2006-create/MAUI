using System;
using System.Collections.Generic;
using PropertyChanged;

namespace Práctica_2._Ahorcado.Modelo {
    [AddINotifyPropertyChangedInterface]
    public class Ahorcado {

        public static List<string> Palabras = new List<string>() { 
            "manzana", "ordenador", "guitarra", "programacion", "desarrollo", "interfaz", "boton", "ventana"
        };

        public string ImagenActual { get; set; } = "img0.jpg";
        public string PalabraVisible { get; set; } = "";
        public int Errores { get; set; }
        public int MaximoErrores { get; set; } = 6;
        public bool JuegoTerminado { get; set; }
        public bool Ganado { get; set; }

        private List<char> palabraCompleta = new();
        private List<char> letrasAdivinadas = new();
        private List<char> letrasFallidas = new();

        public Ahorcado() {
            InicializarJuego();
        }

        public void InicializarJuego() {
            ImagenActual = "img0.jpg";
            Errores = 0;
            JuegoTerminado = false;
            Ganado = false;
            letrasAdivinadas.Clear();
            letrasFallidas.Clear();

            Random random = new();
            int indiceAzar = random.Next(Palabras.Count);
            string palabraSeleccionada = Palabras[indiceAzar];

            palabraCompleta = palabraSeleccionada.ToList();

            // Crear la palabra visible con espacios entre guiones
            var charVisibles = new List<char>();
            for (int i = 0; i < palabraCompleta.Count; i++) {
                charVisibles.Add('_');
                if (i < palabraCompleta.Count - 1) charVisibles.Add(' ');
            }
            // Forzar cambio de propiedad asignando primero vacío
            PalabraVisible = "";
            PalabraVisible = new string(charVisibles.ToArray());
        }

        public bool IntentarLetra(char letra) {
            if (JuegoTerminado || letrasAdivinadas.Contains(letra) || letrasFallidas.Contains(letra)) {
                return false;
            }

            letra = char.ToLower(letra);
            bool esCorrecta = false;

            var charVisibles = PalabraVisible.ToCharArray();

            for (int i = 0; i < palabraCompleta.Count; i++) {
                if (char.ToLower(palabraCompleta[i]) == letra) {
                    charVisibles[i * 2] = palabraCompleta[i]; // i * 2 porque hay espacios entre letras
                    esCorrecta = true;
                }
            }

            PalabraVisible = new string(charVisibles);

            if (esCorrecta) {
                letrasAdivinadas.Add(letra);
            } else {
                letrasFallidas.Add(letra);
                Errores++;
                ActualizarImagen();
            }

            VerificarEstadoJuego();
            return true;
        }

        private void ActualizarImagen() {
            ImagenActual = $"img{Errores}.jpg";
        }

        private void VerificarEstadoJuego() {
            // Verificar si ganó
            if (!PalabraVisible.Contains('_')) {
                Ganado = true;
                JuegoTerminado = true;
                return;
            }

            // Verificar si perdió
            if (Errores >= MaximoErrores) {
                Ganado = false;
                JuegoTerminado = true;
                // Mostrar la palabra completa cuando pierdes
                PalabraVisible = ObtenerPalabraCompleta();
            }
        }

        public string ObtenerPalabraCompleta() {
            return string.Join(" ", palabraCompleta);
        }
    }
}
