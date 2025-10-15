# <div align="center"> ROCK & MOLE </div>
<div align="center">GAME DESIGN DOCUMENT</div>

<br><br><br><br>

**Desarrollado por:**<br>
Banana Slip

<br><br><br>

# 1. INTRODUCCIÓN
   
## 1.1. DESCRIPCIÓN DEL CONTENIDO

*Rock & Mole* es un juego de aventura y acción con vista cenital en 3D, ambientado en un pueblo subterráneo habitado por animales excavadores. El jugador controla a un joven topo que se adentra en una mina compuesta por salas diseñadas manualmente que se reorganizan aleatoriamente en cada run. A lo largo de la exploración, combate enemigos, encuentra eventos especiales y recolecta recursos que pueden venderse en la tienda para conseguir monedas o usarse para mejorar las armas y armaduras.

## 1.2. CARACTERÍSTICAS PRINCIPALES

**Exploración modular y rejugable:** La mina está compuesta por salas diseñadas manualmente que se reorganizan aleatoriamente en cada partida. 

**Combate estratégico y ágil:** El jugador combate con un pico en enfrentamientos cuerpo a cuerpo contra enemigos con patrones definidos, lo que obliga a pensar tácticamente. La dificultad aumenta con la profundidad.

**Mejora persistente de equipo:** Aunque cada run reinicia la mina, el progreso de armas y armaduras se conservan. Los materiales recolectados permiten mejorar el pico, la vestimenta y el casco. Esta progresión persistente motiva al jugador a seguir explorando.

**Eventos especiales y salas únicas:** Durante la exploración el jugador puede encontrar eventos especiales con salas únicas: cofres raros, zonas oscuras, rescates de NPCs o campamentos de descanso. Estos eventos aportan dinamismo y rejugabilidad.

**Gestión y comercio en el pueblo:** Fuera de la mina el jugador interactúa con NPCs que permiten vender objetos y mejorar equipo. El pueblo funciona como centro de operaciones, ofreciendo una pausa entre runs y reforzando el vínculo con la historia.

**Gestión activa de la tienda:** El jugador no solo vende los objetos recolectados, sino que gestiona su propia tienda dentro del pueblo. Puede decidir qué objetos poner a la venta, establecer los precios de cada uno, y observar cómo reaccionan los aldeanos ante sus decisiones comerciales.  Esta mecánica añade una capa estratégica y económica al juego.

**Diseño escalable y expansible:** El juego está diseñado para crecer: nuevos biomas, enemigos, materiales y eventos pueden añadirse fácilmente gracias a una arquitectura modular. Esto permite actualizaciones constantes y contenido adicional sin comprometer la estabilidad del sistema base.

## 1.3. GÉNERO

*Rock & Mole* se encuentra dentro del género de aventura y acción, con presencia de mecánicas de gestión. El jugador no sólo explora y combate, sino que también administra su propia tienda, decide precios y mejora su equipo, añadiendo una capa de estrategia al juego. La estructura del juego cumple con los pilares de un roguelite, en el que cada partida presenta una mina distinta gracias a la reorganización aleatoria de salas, pero con una progresión persistente que permite conservar mejoras en armas y armaduras entre runs.

## 1.4. PLATAFORMAS

*Rock & Mole* se desarrolla como un juego web, accesible desde navegadores en PC y dispositivos móviles. En ordenadores, el jugador podrá utilizar teclado y ratón y en móviles controles táctiles.

## 1.5. ESTILO VISUAL

El estilo visual del juego combina una estética top-down en 3D con un estilo low poly con una ambientación cálida y subterránea. La dirección artística se inspira en títulos como *Moonlighter 2*, especialmente en el uso de colores suaves e iluminación ambiental, con texturas planas y simples.

El protagonista y los NPCs están basados en animales que habitan bajo tierra, como topos o musarañas. Los enemigos representan una fusión entre criaturas animales (corrompidas) y elementos fantásticos.

## 1.6. PROPÓSITO Y PÚBLICO OBJETIVO

El propósito principal del juego es ofrecer una experiencia que combine exploración, combate y gestión económica dentro de un mundo subterráneo. A través de mecánicas roguelite y progresión persistente, se busca mantener la rejugabilidad. 

El juego está dirigido a un público que abarca principalmente jóvenes y adultos entre 13 y 35 años, con interés en juegos de aventura, acción y gestión ligera. Se prioriza la experiencia que pueda disfrutarse tanto en sesiones cortas como en exploraciones más largas, adaptándose a las plataformas disponibles. 

---

# 2. MONETIZACIÓN Y MODELO DE NEGOCIO

El modelo de monetización de *Rock & Mole* se basa en un sistema Buy to Play, es decir, un pago único por el juego base. Esta decisión busca ofrecer una experiencia completa y accesible para el jugador.

Para mantener la viabilidad económica del proyecto a largo plazo, se implementará además una estrategia de monetización mediante DLCs (contenido descargable). Estos se lanzarán periódicamente una vez que la actividad de los jugadores en el juego base comience a disminuir. Cada DLC incluirá contenido significativo como nuevos niveles de la mina, enemigos, materiales recogibles adicionales y mejoras para el equipo del jugador.

Entre los lanzamientos de DLCs, se realizará la publicación de actualizaciones menores gratuitas que mantengan el interés de los jugadores sin requerir grandes esfuerzos de desarrollo. Estas actualizaciones incluirán elementos como una nueva armadura, un enemigo adicional o pequeños ajustes. El objetivo es mantener viva la experiencia de juego sin sobrecargar al equipo de desarrollo ni que suponga un problema económico al jugador.

---

# 3. HISTORIA

El protagonista, un joven topo, vive en un pueblo subterráneo que depende de los minerales de una antigua mina. Tras un misterioso cierre, la mina se volvió peligrosa y oscura. Para salvar a su comunidad y descubrir qué ocurrió, el topo decide adentrarse en sus profundidades, enfrentando criaturas corrompidas y buscando recuperar los recursos perdidos.

---

# 4. MECÁNICAS
## 4.1. JUGABILIDAD

**Estructura de las runs:** Cada partida se desarrolla en una mina subterránea compuesta por salas conectadas. En cada run, el jugador desciende hacia niveles más profundos, enfrentándose a enemigos, recolectando materiales y descubriendo eventos especiales. Las salas están diseñadas manualmente pero se reorganizan de forma aleatoria en cada intento.

Las salas pueden contener enemigos, recursos mineros, cofres o eventos únicos. El objetivo es llegar lo más lejos posible, recolectar materiales valiosos y regresar al pueblo para mejorar el equipo. La partida termina si el jugador es derrotado o si logra llegar al final del nivel y vencer al jefe.

**Dificultad progresiva:** La dificultad aumenta conforme se desciende en la mina. Los enemigos se vuelven más agresivos, aparecen nuevos tipos de enemigos y los recursos se vuelven más escasos. La intensidad de cada run está determinada por la profundidad, el tipo de enemigos y la combinación de salas.

**Combate:** El sistema de combate es en tiempo real, con un enfoque táctico. El jugador ataca con su pico, puede esquivar con un dash y debe aprender los patrones de los enemigos para sobrevivir. Cada criatura tiene comportamientos definidos.

**Recursos y minería:** Los materiales recogidos en la mina tienen un doble propósito: pueden venderse en la tienda gestionada por el jugador para obtener dinero o utilizarse en la herrería y en la tienda de ropa para mejorar el pico, la armadura y el casco. La gestión de estos recursos es clave para progresar en el juego.

**Gestión de la tienda:** Al regresar al pueblo, el jugador puede acceder a su propia tienda, donde gestionar la venta de materiales. Puede organizar los objetos que ha recolectado y establecer el precio de cada uno. Los NPCs del pueblo visitan la tienda y, según el precio, la rareza y su interés, pueden comprar o ignorar los productos. Vender a buen precio permite obtener monedas para invertir en mejoras, pero si los precios son demasiado altos, los aldeanos no comprarán. La tienda evoluciona con el progreso del jugador.

**Progresión del jugador:** Aunque cada run comienza desde cero, el jugador mantiene las mejoras de equipo obtenidas. Esto permite avanzar más en cada intento, enfrentarse a enemigos más fuertes y acceder a salas más complejas.

**Planificación y estrategia:** Antes de cada run, el jugador debe decidir si quiere mantener el equipo actual o invertir en mejoras. No hay selección de habilidades como tal, pero el tipo de armadura, casco y pico equipado influye directamente en el rendimiento.

## 4.2. CONTROLES

El juego está diseñado para jugarse tanto en PC como en dispositivos móviles. En ordenador, el jugador puede utilizar teclado y ratón o con mando tanto de Xbox como de PlayStation. En móvil, el juego requiere el uso de mando conectado, ya que no está optimizado para controles táctiles.

| Acción | Teclado y ratón (PC) | Mando Xbox | Mando PlayStation |
| :----: | :------------------: | :--------: | :---------------: |
| Moverse | Teclas WASD         | Joystick izquierdo | Joystick izquierdo |
| Atacar | Click izquierdo | RT (gatillo derecho) | R2 (gatillo derecho) |
| Dash | Shift | LT (gatillo izquierdo) | L2 (gatillo izquierdo) |

## 4.3. PERSONAJES

**Protagonista:**

* **Nombre:** (Por determinar).
* **Especie:** Topo.
* **Rol:** Explorador y comerciante. Es el personaje controlado por el jugador.

**Vendedor:**

* **Nombre:** Telmo.
* **Especie:** Ratón.
* **Rol:** Compra objetos del jugador y vende armaduras y vestimentas. Telmo regenta la tienda de ropa del pueblo. Aunque su local parece especializado en moda, es el lugar donde el jugador puede vender los materiales recogidos en la mina y adquirir nuevas piezas de equipo.

**Herrero:**

* **Nombre:** Bruno.
* **Especie:** Topo.
* **Rol:** Mejora armas. Bruno es el herrero del pueblo. Se encarga de reforzar el pico, la armadura y el casco del jugador utilizando los materiales obtenidos en la mina. Su taller es el corazón del progreso mecánico del juego.

**Aldeanos:**

* **Especie:** Variadas (musarañas, ratones, topos, tejones, etc).
* **Rol:** Ambientación, dan consejos, reaccionan al progreso del jugador. Los aldeanos aportan vida al pueblo. Algunos ofrecen consejos útiles, otros comentan los avances del jugador.

## 4.4. ENEMIGOS
### 4.4.1. GOLEMS

* **Tipo:** Enemigo estándar.
* **Comportamiento:** Cuando el jugador entra en su área de detección, el golem lo persigue y ataca.
* **Características:** daño y velocidad moderados. Su presencia es común en las salas de combate.
* **Función:** Introduce al jugador en el sistema de combate básico y sirve como enemigo recurrente.

**Golem pequeño:** 

* **Tipo:** Enemigo menor
* **Comportamiento:** Persigue al jugador si entra en su área, pero con menor alcance.
* **Características:** Menor daño y velocidad. Aparecen en grupo o como resultado de la división del golem grande.
* **Función:** Añade presión en combate por número. Su baja resistencia permite al jugador practicar esquivas y ataques rápidos.

**Golem grande:**

* **Tipo:** Mini-jefe de nivel.
* **Comportamiento:** Posee un área de detección más amplia y causa mayor daño.
* **Características:** Al ser derrotado, se divide en dos golems pequeños, lo que prolonga el combate.
* **Función:** Marca el final de un nivel. Su derrota permite completar la run actual. Representa un desafío táctico por su tamaño y transformación.

## 4.5. ARMAS Y ARMADURAS

La progresión del jugador se basa en la mejora del equipo mediante los materiales recolectados en la mina. Existen tres tipos de elementos equipables: arma (pico), vestimenta (torso) y casco, cada uno con efectos específicos sobre el rendimiento del jugador. Las mejoras se realizan en el pueblo, a través de la herrería y la tienda de ropa.

### 4.5.1. MEJORA DE ARMA - PICO

El pico es el arma principal del jugador. Se mejora en la herrería, utilizando materiales específicos. Cada nivel incrementa el daño por segundo y la velocidad de ataque.

| Nivel | Nombre | Efecto | Precio |
| :---: | :----: | :----: | :----: |
| 1 | Pico oxidado | Daño básico | Arma inicial |
| 2 | Pico reforzado | +10% daño y +10% velocidad de ataque | 70 monedas y 15 lingotes de hierro |
| 3 | Pico afilado | +15% daño y +20% velocidad de ataque | 100 monedas, 25 lingotes de hierro y 5 piedras de carbón |
| 4 | Pico endurecido | +20% daño y +30% velocidad de ataque | 200 monedas, 40 lingotes, 10 piedras de carbón y 5 piedras de obsidiana |
| 5 | Pico maestro | +30% daño y +40% velocidad de ataque | 300 monedas, 45 lingotes de hierro, 15 piedras de carbón y 15 piedras de obsidiana |

### 4.5.2. VESTUARIO - PRENDAS DEL TOPO

Las prendas modifican la velocidad de movimiento y la defensa del jugador. Se adquieren en la tienda de ropa, utilizando rollos de tela o lingotes de bronce.

| Nombre | Efecto | Precio |
| :---: | :----: | :----: |
| Chaleco reforzado | Sin efectos | Prenda inicial |
| Túnica de minero | +15% velocidad y -25% defensa | 300 monedas y 30 rollos de tela |
| Armadura pesada | +25% defensa y -15% velocidad | 300 monedas y 50 lingotes de bronce | 

### 4.5.3. VESTUARIO - CASCOS

Los cascos afectan la iluminación, la defensa y la velocidad. Se fabrican en la tienda de ropa, utilizando lingotes de bronce y cristales de cuarzo.

| Nombre | Efecto | Precio |
| :---: | :----: | :----: |
| Casco de minero | Iluminación básica | Casco inicial |
| Casco reforzado | +50% iluminación, +10% defensa y -5% velocidad | 200 monedas, 10 lingotes de bronce y 5 piedras de cuerzo |
| Casco blindado | +50% iluminación, +25% defensa y -5% velocidad | 800 monedas, 50 lingotes de bronce y 15 piedras de cuarzo |
| Casco ligero | +50% iluminación y +5% velocidad | 180 monedas, 10 lingotes de bronce y 5 piedras de cuarzo |

### 4.5.4. MATERIALES RECOGIBLES

Durante la exploración de la mina, el jugador puede recolectar materiales que se dividen en dos categorías:

**Materiales de venta:** Valiosos por su rareza o belleza, se venden en la tienda de ropa para obtener monedas.

| Nombre | Rareza | Precio |
| :---: | :----: | :----: |
| Ámbar | Común | 8 monedas |
| Esmeralda | Raro | 10 monedas |
| Rubí | Raro | 15 monedas |
| Diamante | Muy raro | 100 monedas |
<br>

**Materiales de mejora:** Utilizados para mejorar el equipo, aunque también pueden venderse.

| Nombre | Utilidad | Rareza | Precio |
| :---: | :----: | :----: | :-------: |
| Lingotes de hierro | Mejora de armas | Común | 4 monedas |
| Rollos de tela | Fabricación de prendas | Común | 30 monedas |
| Carbón | Mejora de armas | Raro | 8 monedas |
| Lingotes de bronce | Fabricación de armaduras y cascos | Raro | 8 monedas |
| Cristales de cuarzo | Fabricación de cascos | Muy raro | 15 monedas |
| Cristales de obsidiana | Mejora de armas | Muy raro | 15 monedas |

## 4.6. NIVELES, EVENTOS Y MISIONES
### 4.6.1. NIVELES DE PROFUNDIDAD

La mina está dividida en niveles progresivos, cada uno con su propio entorno, enemigos y materiales. Cada vez que el jugador vence al jefe final de un nivel, se desbloquea el siguiente, aumentando la dificultad y la variedad de desafíos. Los niveles presentan diferencias visuales, mecánicas y estratégicas, incentivando la exploración y la mejora constante del equipo.

### 4.6.2. EVENTOS

Durante la exploración de la mina, el jugador puede encontrarse con salas especiales que aparecen de forma aleatoria, según un porcentaje de aparición predefinido. Estos eventos aportan variedad, sorpresa y decisiones tácticas que enriquecen el desarrollo de cada run.

**Sala del Tesoro:** Esta sala aparece sin enemigos. En el centro se encuentra un gran cofre que puede contener objetos raros, materiales valiosos o grandes cantidades de dinero. Su aparición es poco frecuente

**Sala Oscura:** En esta sala no hay iluminación ambiental, por lo que el jugador debe depender exclusivamente de la luz que emite su casco. Los enemigos se ocultan en las sombras, dificultando su detección y aumentando la tensión del combate.

**Sala de Rescate:** Aquí el jugador encuentra a un NPC atrapado por enemigos. Para completar el evento, debe enfrentarse a los enemigos que rodean al personaje y liberarlo. Si lo consigue, el NPC puede ofrecer una recompensa o iniciar un intercambio.

**Sala de Campamento:** Esta sala ofrece un momento de calma dentro de la mina. En ella, el jugador encuentra un pequeño campamento con una hoguera encendida. Junto a la hoguera se encuentra Pico, el canario del tutorial, que actúa como personaje de apoyo. Si el jugador habla con él, puede recuperar parte de su salud, lo que convierte esta sala en un punto estratégico para continuar la exploración.

### 4.6.3. MISIONES

Las misiones se integran dentro de los eventos, especialmente en las salas de rescate. Cuando el jugador encuentra un NPC atrapado, se activa la misión de rescatarlo de los enemigos. Si tiene éxito, el NPC puede ofrecer una recompensa directa, un intercambio de objetos o desbloquear contenido adicional. Estas misiones aportan variedad narrativa y recompensas únicas dentro de la estructura de las runs.

5. INTERFAZ
5.1. DIAGRAMA DE FLUJO

A continuación se muestra el diagrama de flujo de las interfaces del juego, donde se representa cómo se conectan entre sí las distintas pantallas y menús.
