# Un juego de Artilleria
En las lejanas tierras de **Yuca** la tribu de **Green Bel** vive prospera y armoniosamente. Sus habitantes, amables y valerosos son reconocidos por su habilidad con el arco. Desgraciadamente no están solos. Los goblins de la tribu **Red Belvet** son ambiciosos y su única obsesión es dominar el territorio. No son conocidos por su gran habilidad con el arco, pero sí por su enorme población. Infestan la jungla con su irada sonrisa y buscan exterminar a cualquier Green Bel que se interponga en su camino.
Sobrevivir en este terreno hostil es básico para la supervivencia de los Green Bel, y sólo una punteria certera asegurará su futuro.
****
## Regals del juego
**Emboscada en la jungla** es un juego de artilleria, dónde tu objetivo es acabar con todos tus enemigos antes de que ellos acaben contigo. El arco es la única arma de la que dispones. No olvides que los goblins por naturaleza no són rápidos ni ágiles, así que tendrás que confiar en tu punteria.
#### Yingo
Tomarás el control de **Yingo**, un goblin Green Bel que intenta defender a sus compañeros. Dispones de tres acciones principales:
* **Andar**: Puedes desplazarte a un lado u otro usando las teclas 'derecha' u 'izquierda' de tu teclado.
* **Saltar**: Realiza un pequeño salto presionando la barra espaciadora.
* **Apuntar**: Puedes apuntar para modificar la trayectoria de tu proyectil. Usa las teclas de 'arriba' y 'abajo' de tu teclado para ajustar el angulo de tiro.
* **Disparar**: El disparo puede ser de varias intensidades, según necesites un disparo a largo o corto alcance. El disparo se produce al presionar el **botón izquierdo del ratón**. Cuanto más tiempo se mantenga presionado el ratón, más alcance tendrá el disparo. Fíjate en el marcador que indica la intensidad de tu disparo.
Yingo resiste hasta **ocho imapctos** de proyectil.
#### Red Belvet
Tus enemigos estan posicionados en puntos estratégicos, y no estan dispuestos a moverse de allí. Su principal acción es el disparo. Acaba con ellos para ganar la partida.
Los Red Belvet resisten hasta **cuatro impactos** de proyectil.
#### Yuca
Yuca es un rocoso territorio de costa. Es muy conocido por sus rocas flotantes y sus dragones. Las rocas más elevadas son siempre las zonas predilectas para los goblins, ya que les ofrece un buen ángulo de tiro. Pero no olvides que una roca también puede ser un obstáculo para conseguir un buen tiro.
### Los turnos de juego
Los turnos se producen seguidamente uno detrás del otro. En cada turno solo un único personaje será capaz de realizar sus acciones. Cuando un turno termina empieza el turno del seguiente personaje. Hay dos formas de terminar un turno:
* El contador de tiempo llega a 0. Una vez agotado el tiempo, el turno salta al siguiente personaje.
* Se completa la acción de disparo.
Durante el turno de Yingo, mientras quede tiempo podrá hacer cualquier de las acciones disponibles, pero recuerda que una vez se realiza el disparo, tu turno termina.
### Ganar / perder el juego
No hay límite de turnos. Para ganar el juego tienes que acabar con todos tus enemigos. Por otro lado si te quedas sin vida, el juego habrá terminado. Hay dos formas de perder toda la vida:
* Caes de cualquier roca flotante
* Recibes ocho impactos de proyectil
*****
## Estructura del proyecto
### Recursos
* Sprites:
* * https://assetstore.unity.com/packages/2d/environments/free-asset-2d-handcrafted-art-117049 (Escenario)
* * https://assetstore.unity.com/packages/2d/characters/goblin-archer-cartoon-character-17253 (Goblin)
* * https://opengameart.org/content/flappy-dragon-sprite-sheets (Dragon)
* Tipografia:
* * https://befonts.com/orc-horde-bb-font-family.html
* Musica y sonidos:
* * https://soundimage.org/fantasy-8/
* * https://www.freesoundeffects.com/free-sounds/jungle-10009/
* * https://www.freesoundeffects.com/free-sounds/applause-10033/
### Assets
La estrucutra de los Assets contiene los siguientes elementos:
- Animations: Contiene todas las animaciones de los personajes creadas por Unity.
- Fonts: Contiene las fuentes que se usan para los elementos de UI.
- Prefabs: Elementos que se repiten en el juego. Algunos de ellos se instancian directamente mediante scripts.
- Music: Contiene la banda sonora principal. Son musicas que no se cargan por script.
- Resources: Contiene principalmente los sonidos que se cargan en su mayoria mediante scripts.
- Scenes: Contiene una única escena.
- Scripts: Mantiene todos los scripts del proyecto.
- Sprites: Contiene todos los Sprites del juego.
### Scripts y sus elementos
Cada uno de los scripts gestiona una parte del juego. Veamos cuales son su funcionalidades principales y que elementos gestionan.
#### Los Goblin
Conforman todos los personajes presentes en el juego, ya sea el goblin protagonista como los enemigos. Todos los estados (andar, saltar, disparar y morir) se gestionan mediante una máquina de estados que controla la animación correspondiente. 
Hay tres scripts que controlan el comportamiento de estos elementos:
* **PlayerController:** Es la clase principal en el control de los Goblin. Se encarga de gestionar aquellos comportamientos comunes, ya sean del goblin héroe o de los enemigos. Acciones como orientarse en la posición correcta o apuntar. También recoge los atributos que luego se usaran en los controladores hijo.
* **HeroController**: Extiende de **PlayerController**. Se encarga de gestionar las acciones que són propias del Goblin heroe. Principalmente activa las acciones de saltar, andar, apuntar o disparar como respuesta de las acciones de teclado.
* **EnemyController:** También extiende de **PlayerController**. Gestiona las acciones de los goblins enemigos, básicamente apuntar y disparar. En este caso no responde a acciones de teclado, sinó que realiza los cálculos necesarior para que el Goblin sea capaz de apuntar de una forma más o menos precisa y realizar el disparo de forma automática.
#### Las armas
El arco y la flecha son las armas principales del juego. El arma se instancia directamente en el personaje y se controla mediante su propio script, por lo que se podrian asignar fácilmente otras armas a los Goblin, simplemente instanciando cualquier otro Prefab de arma con su controlador.
En el caso del arco su controlador es:
* **WeaponController:** Su principal función es efectuar la acción de disparo. Para ello pone a disposición del controlador de personajes las acciones de **FireDown** y **FireUp**. Es este método el encargado de calcular la velocidad en que el proyectil será disparado y ejecutar dicho disparo. También se encarga de mostrar una guia visual para conocer la fuerza del disparo.
WeaponController podria disparar cualquier tipo de proyectil que se le asigne. Por defecto instancia el elemento flecha que contiene el siguiente controlador:
* * **ArrowController:** Gestiona el comportamiento propio de la flecha como el cálculo de la inclinación para conseguir un efecto real de la propia física de la flecha o el control de la explosión cuando esta colisiona con un elemento. La ventaja de tener un controlador propio para la flecha es que permite que el arma cargue cualquier otro tipo de proyectil en un futuro.
#### La vida
El nivel de vida está vingulado a cada uno de los personajes del juego. Se gestiona mediante el siguiente controlador:
* **HealthBarController:** Controla en todo momento la vida restante del personaje al que se vincula. Ejecuta la muerte del personaje cuando su salud llega a 0.
#### La cámara
* **CameraController:** Controla principalmente las tres principales acciones de la cámara:
1. Seguir al personaje principal: Cuando el turno es del goblin héroe, la cámara le seguirá en todo momento.
2. Posicionarse en la posición del jugador que tiene el turno en ese momento.
3. Realizar un Zoom out cuando se dispare un proyectil para poder apreciar el recorrido de este.
### El juego
* **GameController:** Orquesta el funcionamiento del juego. Su misión principal es conocer todos los personajes que hay en el juego y distribuir correctamente los turnos uno detrás de otro. Pone en marcha el contador de tiempo que determina la duración de un turno y conecta múltiples elementos para ejecutar acciones en concordancia con el turno. Por ejemplo movimientos de cámara. Es también el encargado de decidir el ganador o perdedor del juego.
En relación a este controlador, encontramos otros encargados de la gestión de la interfície:
* * **UIController:** Se encarga de informar del estado del juego al jugador mediante elementos de interfície. Elementos como el contador o texto de fin de juego (ganar / perder).
* * **ButtonController:** Permite realizar otra partida cuando la actual ha terminado.
### Escenario
* **DragonController:** Añade dragones en el escenario. Los incorpora desde dos puntos de salida distribuidos en la escena y les incorpora dirección y velocidad.
* **DeadZoneController:** Detecta si el Goblin héroe ha caído de una de las rocas y finaliza el juego.
****
## Preview
[![Demo](https://i9.ytimg.com/vi/DwGRMzfg9NQ/mq2.jpg?sqp=CJiQ8ZkG&rs=AOn4CLAQUMZpnaZQ_kYbgQM2VJlSbpDbdw)](https://youtu.be/DwGRMzfg9NQ)
