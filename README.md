# <div align="center"> ROCK & MOLE </div>
<div align="center">GAME DESIGN DOCUMENT - v0.2.0</div>

<br><br><br><br>

**Desarrollado por:**<br>
Banana Slip

<br><br><br>

# 1. INTRODUCCIÓN
   
## 1.1. DESCRIPCIÓN DEL CONTENIDO

*Rock & Mole* es un juego de aventura y acción con vista isométrica en 3D, ambientado en un pueblo subterráneo habitado por animales excavadores. El jugador controla a un joven topo que se adentra en una mina compuesta por salas diseñadas manualmente que se reorganizan aleatoriamente en cada run. A lo largo de la exploración, combate enemigos, encuentra eventos especiales y recolecta recursos que pueden venderse en la tienda para conseguir monedas o usarse para mejorar las armas y armaduras.

## 1.2. CARACTERÍSTICAS PRINCIPALES

**Exploración modular y rejugable:** La mina está compuesta por salas diseñadas manualmente que se reorganizan aleatoriamente en cada partida. 

**Combate estratégico y ágil:** El jugador combate con un pico en enfrentamientos cuerpo a cuerpo contra enemigos con patrones definidos, lo que obliga a pensar tácticamente. La dificultad aumenta con la profundidad.

**Mejora persistente de equipo:** Aunque cada run reinicia la mina, el progreso de armas y armaduras se conservan. Los materiales recolectados permiten mejorar el pico, la vestimenta y el casco. Esta progresión persistente motiva al jugador a seguir explorando.

**Eventos especiales y salas únicas:** Durante la exploración el jugador puede encontrar eventos especiales con salas únicas: cofres raros, zonas oscuras, rescates de NPCs o campamentos de descanso. Estos eventos aportan dinamismo y rejugabilidad.

**Gestión y comercio en el pueblo:** Fuera de la mina el jugador interactúa con NPCs que permiten vender objetos y mejorar equipo. El pueblo funciona como centro de operaciones, ofreciendo una pausa entre runs y reforzando el vínculo con la historia.

**Diseño escalable y expansible:** El juego está diseñado para crecer: nuevos biomas, enemigos, materiales y eventos pueden añadirse fácilmente gracias a una arquitectura modular. Esto permite actualizaciones constantes y contenido adicional sin comprometer la estabilidad del sistema base.

## 1.3. GÉNERO

*Rock & Mole* se encuentra dentro del género de aventura y acción, con presencia de mecánicas de gestión. El jugador no sólo explora y combate, sino que también administra su propia tienda, decide precios y mejora su equipo, añadiendo una capa de estrategia al juego. La estructura del juego cumple con los pilares de un roguelite, en el que cada partida presenta una mina distinta gracias a la reorganización aleatoria de salas, pero con una progresión persistente que permite conservar mejoras en armas y armaduras entre runs.

## 1.4. PLATAFORMAS

*Rock & Mole* se desarrolla como un juego web, accesible desde navegadores en PC y dispositivos móviles. En ordenadores, los jugadores pueden utilizar teclado y ratón, o bien jugar con mando y en móviles, sólo es posible conectar un mando externo para jugar.

## 1.5. ESTILO VISUAL

El estilo visual del juego combina una estética low- poly en 3D con vista isométrica y una ambientación cálida y subterránea. La dirección artística se inspira en títulos como *Moonlighter 2*, especialmente en el uso de colores suaves e iluminación ambiental, con texturas planas y simples.

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

El juego está diseñado para jugarse tanto en PC como en dispositivos móviles. En ordenador, el jugador puede utilizar teclado y ratón o con mando de Xbox, PlayStation y Nintendo. En móvil, el juego requiere el uso de mando conectado, ya que no está optimizado para controles táctiles.

| Acción | Teclado y ratón (PC) | Mando Xbox | Mando PlayStation | Mando Nintendo |
| :----: | :------------------: | :--------: | :---------------: | :-------------: |
| Moverse | Teclas WASD         | Joystick izquierdo | Joystick izquierdo | Joystick izquierdo |
| Atacar | Click izquierdo | RT (gatillo derecho) | R2 (gatillo derecho) | ZR (gatillo derecho) |
| Dash | Shift | LT (gatillo izquierdo) | L2 (gatillo izquierdo) | ZL (gatillo izquierdo) |
| Interactuar | E | X | □ | Y |  

<div align="center"><p><i>Tabla 1: Controles de personaje.</i></p></div>

<br>  

| Interfaz | Teclado y ratón (PC) | Mando Xbox | Mando PlayStation | Mando Nintendo |
| :----: | :------------------: | :--------: | :---------------: | :-------------: |
| Pausa | Esc         | ≡ | Options | + |
| Inventario | Tab | Y | △ | X |
| Aceptar | Clic | A | X | A |
| Diálogos | Espacio/Clic | A | X | A |  

<div align="center"><p><i>Tabla 2: Controles de interfaces.</i></p></div>

<br>

## 4.3. PERSONAJES

**Protagonista:**

* **Nombre:** Otto.
* **Sexo**: Masculino.
* **Especie:** Topo.
* **Edad**: 22 años.
* **Personalidad:** Otto es un personaje tranquilo y sereno, capaz de mantener la calma incluso en situaciones extremas. Esta actitud le permite tomar decisiones con cabeza fría, aunque a veces lo lleva a asumir riesgos que otros evitarían. Tiene una curiosidad natural y un fuerte sentido de responsabilidad hacia su comunidad, lo que lo impulsa a seguir adelante pese al peligro.
* **Aspecto físico:** De estatura y complexión media, Otto viste con ropa de trabajo desgastada por el uso: una camiseta y un peto cubiertos de tierra y marcas de desgaste. Lleva siempre un casco de minero naranja con luz frontal, esencial para explorar zonas oscuras. Su mochila grande y resistente le permite transportar los objetos que encuentra en sus expediciones por la mina.
* **Rol:** Otto es el personaje principal controlado por el jugador. Su rol combina exploración y comercio: recorre las profundidades de la mina en busca de recursos, enfrentando peligros y recolectando objetos que luego puede intercambiar para ayudar a su comunidad.
* **Trasfondo:** Vive en un pueblo subterráneo que depende de los minerales de una antigua mina. Tras un misterioso cierre, la mina se volvió peligrosa y oscura. Para salvar a su comunidad y descubrir qué ocurrió, Otto decide adentrarse en sus profundidades, enfrentando criaturas corrompidas y buscando recuperar los recursos perdidos.

**Vendedor:**

* **Nombre:** Telmo.
* **Sexo**: Masculino.
* **Especie:** Topo.
* **Edad:** 28 años.
* **Personalidad:** Telmo es extrovertido, hablador y siempre está al tanto de las últimas tendencias, incluso bajo tierra. Tiene un gran sentido del humor y una actitud comercial muy marcada: siempre intenta convencer al jugador de que necesita una nueva prenda. Aun así, se preocupa por el bienestar del pueblo y colabora activamente con los exploradores.
* **Aspecto físico:** Telmo viste con ropa llamativa y colorida. Su tienda está decorada con telas, maniquíes y luces cálidas que contrastan con el resto del pueblo.
* **Rol:** Compra objetos del jugador y vende armaduras y vestimentas. Telmo regenta la tienda de ropa del pueblo. Aunque su local parece especializado en moda, es el lugar donde el jugador puede vender los materiales recogidos en la mina y adquirir nuevas piezas de equipo.
* **Trasfondo**: Antes del cierre de la mina, Telmo se dedicaba exclusivamente a la moda subterránea. Sin embargo, con la crisis de recursos, adaptó su negocio para ayudar a los exploradores, convirtiendo su tienda en un punto clave para el intercambio de materiales y la mejora del equipo.

**Herrero:**

* **Nombre:** Bruno.
* **Sexo**: Masculino.
* **Especie:** Topo.
* **Edad:** 45 años.
* **Personalidad:** Bruno es serio, meticuloso y reservado. Habla poco, pero cuando lo hace, sus palabras son precisas. Tiene un fuerte sentido del deber y se toma muy en serio su trabajo como herrero. Aunque no lo demuestre abiertamente, se preocupa por la seguridad del jugador y del pueblo.
* **Aspecto físico:** De complexión robusta, Bruno siempre está cubierto de hollín. Lleva un mandil de cuero y guantes gruesos. Su taller está lleno de herramientas, yunques y brasas encendidas, con un ambiente cálido y ruidoso.
* **Rol:** Mejora armas. Bruno es el herrero del pueblo. Se encarga de reforzar el pico, la armadura y el casco del jugador utilizando los materiales obtenidos en la mina. Su taller es el corazón del progreso mecánico del juego.
* **Trasfondo:** Bruno proviene de una larga tradición de herreros subterráneos. Desde joven ha trabajado el metal, y su habilidad para forjar herramientas resistentes lo convirtió en una figura clave del pueblo. Con la mina cerrada, su papel se volvió aún más importante, ya que sin su ayuda, los exploradores no podrían mejorar su equipo ni sobrevivir a los peligros del subsuelo.

**Aldeanos:**

* **Especie:** Variadas (musarañas, ratones, topos, tejones, etc).
* **Personalidad:** Los aldeanos son diversos en carácter y actitud. Algunos son curiosos y entusiastas, otros más temerosos o escépticos. En conjunto, representan el alma del pueblo: una comunidad unida que observa con atención los avances del jugador y reacciona a sus logros o fracasos.
* **Rol:** Ambientación, dan consejos, reaccionan al progreso del jugador. Los aldeanos aportan vida al pueblo. Algunos ofrecen consejos útiles, otros comentan los avances del jugador.
* **Trasfondo:** Los aldeanos han vivido durante generaciones en el pueblo subterráneo, dependiendo de la mina como fuente de recursos. El cierre repentino los dejó en una situación crítica, pero mantienen la esperanza gracias a los esfuerzos del jugador. Su presencia constante refuerza la sensación de comunidad y da contexto emocional al progreso del juego.

<br>

  
## 4.4. ENEMIGOS
### 4.4.1. GÓLEMS

* **Tipo:** Enemigo estándar.
* **Comportamiento:** Cuando el jugador entra en su área de detección, el gólem lo persigue y ataca.
* **Características:** daño y velocidad moderados. Su presencia es común en las salas de combate.
* **Función:** Introduce al jugador en el sistema de combate básico y sirve como enemigo recurrente.

**Gólem pequeño:** 

* **Tipo:** Enemigo menor.
* **Trasfondo:** Estos pequeños enemigos habitan en la mina como extensiones fragmentadas del gólem de roca. No tienen voluntad propia ni inteligencia, pero actúan por instinto defensivo. Su origen suele estar ligado a la división de un gólem mayor tras ser derrotado, aunque también pueden aparecer por sí solos en zonas corrompidas.
* **Características:** Los fragmentos de gólem tienen menor daño y velocidad que otros enemigos, pero suelen aparecer en grupo, lo que aumenta la presión sobre el jugador. Su tamaño reducido y baja resistencia los convierte en blancos fáciles, aunque su número puede complicar el combate si no se gestionan bien. Su aspecto recuerda a pequeñas criaturas de piedra, con formas irregulares que conservan parte de la textura del gólem original.
* **Comportamiento:** Persigue al jugador si entra en su área, pero con menor alcance.
* **Función:** Añade presión en combate por número. Su baja resistencia permite al jugador practicar esquivas y ataques rápidos.

**Gólem grande:**

* **Tipo:** Enemigo estándar.
* **Trasfondo:**  El gólem de roca habita en las profundidades de la mina desde tiempos antiguos. Su existencia está ligada a la protección del entorno subterráneo. No tiene conciencia ni emociones: su único propósito es defender la mina de cualquier intruso. Con el cierre misterioso de la mina, su comportamiento se ha vuelto más agresivo, atacando a todo aquel que se acerque.
* **Características:**  Tiene un daño y una velocidad moderados, lo que lo convierte en una amenaza constante pero manejable para el jugador. Su gran tamaño y cuerpo formado por bloques de roca le dan una apariencia imponente, aunque mantiene una silueta natural que se integra con el entorno de la mina. Es común encontrarlo en las salas de combate, donde aparece con frecuencia como enemigo recurrente.
* **Comportamiento:** Cuando el jugador entra en su área de detección, el gólem lo persigue y ataca.
* **Función:** Introduce al jugador en el sistema de combate básico y sirve como enemigo recurrente.
<br>

### 4.4.2. RATAS

* **Tipo:** Enemigo a distancia.
* **Trasfondo:** Estas ratas han sido corrompidas por la energía cristalina que emana de las profundidades de la mina. Su cuerpo está parcialmente cubierto por fragmentos de cristal que han alterado su comportamiento y les han otorgado habilidades ofensivas. Antes eran simples animales, pero ahora defienden la mina con una agresividad inusual, como si estuvieran conectadas a su núcleo.
* **Características:** De tamaño pequeño y movimientos ágiles, las ratas tienen baja resistencia pero compensan con ataques a distancia. Los cristales incrustados en su cuerpo les permiten lanzar proyectiles afilados que dañan al jugador desde lejos. Su aspecto es inquietante: pelaje desgastado, ojos brillantes y zonas del cuerpo cubiertas por cristales que emiten un leve resplandor.
* **Comportamiento:** Las ratas vagan por la sala hasta que el jugador entra en su área de detección. En ese momento, se detienen y comienzan a disparar cristales desde la distancia. Si el jugador abandona su área, dejan de atacar y vuelven a su patrón de movimiento errático.
* **Función:** Introducen el combate a distancia y obligan al jugador a moverse, esquivar y priorizar objetivos. Su presencia en salas combinadas con enemigos cuerpo a cuerpo añade variedad táctica y refuerza la necesidad de gestionar el espacio.
<br>

### 4.4.3. BARRILES

* **Tipo:** Enemigo Kamikaze.
* **Trasfondo:** Estos conejos han encontrado una forma peculiar de defender la mina: se esconden dentro de barriles reforzados y se lanzan contra los intrusos. Se desconoce si actúan por voluntad propia o si han sido manipulados por la corrupción que afecta a la mina. Lo que está claro es que su método es tan absurdo como efectivo.
* **Características:** De tamaño medio, los barriles explosivos se mueven rápidamente en línea recta hacia el jugador. Al impactar o al acercarse lo suficiente, detonan causando daño en área. Su diseño es cómico pero peligroso: un barril con patas, orejas asomando por la tapa y una mecha encendida que indica su inminente explosión.
* **Comportamiento:** Al detectar al jugador, corren directamente hacia él sin detenerse. No atacan de forma convencional, sino que explotan al contacto o tras unos segundos. Si el jugador logra esquivarlos, pueden explotar sin causar daño, pero en espacios cerrados su amenaza aumenta.
* **Función:** Sirven para añadir tensión y dinamismo en combate. Obligan al jugador a moverse constantemente y a estar atento al entorno.
<br>

## 4.5. ARMAS Y ARMADURAS

La progresión del jugador se basa en la mejora del equipo mediante los materiales recolectados en la mina. Existen tres tipos de elementos equipables: arma (pico), vestimenta (torso) y casco, cada uno con efectos específicos sobre el rendimiento del jugador. Las mejoras se realizan en el pueblo, a través de la herrería y la tienda de ropa.

### 4.5.1. MEJORA DE ARMA - PICO

El pico es el arma principal del jugador. Se mejora en la herrería, utilizando materiales específicos. Cada nivel incrementa tanto el daño y la velocidad de ataque, como la probabilidad y el daño de los ataques críticos. 

| Nivel | Nombre | Efecto | Precio de mejora |
| :---: | :----: | :----: | :----: |
| 1 | Pico oxidado | 10 puntos de daño básico, x1.25 de daño crítico, 5% de probabilidad de crítico y 0% de velocidad de ataque extra | Arma inicial |
| 2 | Pico reforzado | 12 puntos de daño básico, x2 de daño crítico, 6% de probabilidad de crítico y 5% de velocidad de ataque extra | 70 monedas y 15 lingotes de hierro |
| 3 | Pico afilado | 15 puntos de daño básico, x2.5 de daño crítico, 8% de probabilidad de crítico y 10% de velocidad de ataque extra | 100 monedas, 25 lingotes de hierro y 5 piedras de carbón |
| 4 | Pico endurecido | 20 puntos de daño básico, x3 de daño crítico, 10% de probabilidad de crítico y 15% de velocidad de ataque extra | 200 monedas, 40 lingotes, 10 piedras de carbón y 5 piedras de obsidiana |
| 5 | Pico maestro | 30 puntos de daño básico, x5 de daño crítico, 15% de probabilidad de crítico y 20% de velocidad de ataque extra | 300 monedas, 45 lingotes de hierro, 15 piedras de carbón y 15 piedras de obsidiana |

<div align="center"><p><i>Tabla 3: Mejoras de armas.</i></p></div>

<br>

### 4.5.2. VESTUARIO - PRENDAS DEL TOPO

Las prendas modifican la velocidad de movimiento, velocidad de ataque y la defensa del jugador. Se adquieren en la tienda de ropa, utilizando rollos de tela y lingotes de bronce.

| Nombre | Efecto | Precio |
| :---: | :----: | :----: |
| Chaleco reforzado | 100 puntos de vida, +0% velocidad y +0% velocidad de ataque | Prenda inicial |
| Túnica de minero | 60 puntos de vida, +30% velocidad y +30% velocidad de ataque | 300 monedas, 30 rollos de tela y 20 lingotes de bronce |
| Armadura pesada | 170 puntos de vida, -30% velocidad y -30% velocidad de ataque | 300 monedas, 10 rollos de tela y 60 lingotes de bronce | 

<div align="center"><p><i>Tabla 4: Prendas del torso.</i></p></div>

<br>
  
### 4.5.3. VESTUARIO - CASCOS

Los cascos afectan la defensa, la velocidad y la velocidad de ataque. Se fabrican en la tienda de ropa, utilizando lingotes de bronce y cristales de cuarzo.

| Nombre | Efecto | Precio |
| :---: | :----: | :----: |
| Casco de minero | 50 puntos de vida, +0% velocidad y +0% velocidad de ataque | Casco inicial |
| Casco reforzado | 70 puntos de vida, +10% velocidad y +10% velocidad de ataque | 200 monedas, 10 lingotes de bronce y 5 piedras de cuerzo |
| Casco blindado | 120 puntos de vida, -20% velocidad y +30% velocidad de ataque | 800 monedas, 50 lingotes de bronce y 15 piedras de cuarzo |
| Casco ligero | 50 puntos de vida, +30% velocidad y +50% velocidad de ataque | 500 monedas, 30 lingotes de bronce y 10 piedras de cuarzo |

<div align="center"><p><i>Tabla 5: Cascos.</i></p></div>

<br>
  
### 4.5.4. MATERIALES RECOGIBLES

Durante la exploración de la mina, el jugador puede recolectar materiales que se dividen en dos categorías:

**Materiales de venta:** Valiosos por su rareza o belleza, se venden en la tienda de ropa para obtener monedas.

| Nombre | Rareza | Beneficio |
| :---: | :----: | :----: |
| Ámbar | Común | 8 monedas |
| Esmeralda | Raro | 50 monedas |
| Rubí | Raro | 70 monedas |
| Diamante | Muy raro | 100 monedas |

<div align="center"><p><i>Tabla 6: Materiales para la venta.</i></p></div>

<br>

**Materiales de mejora:** Utilizados para mejorar el equipo, aunque también pueden venderse.

| Nombre | Utilidad | Rareza | Beneficio |
| :---: | :----: | :----: | :-------: |
| Lingotes de hierro | Mejora de armas | Común | 4 monedas |
| Carbón | Mejora de armas | Raro | 15 monedas |
| Lingotes de bronce | Fabricación de armaduras y cascos | Raro | 25 monedas |
| Cristales de cuarzo | Fabricación de cascos | Muy raro | 50 monedas |
| Rollos de tela | Fabricación de prendas | Muy raro | 50 monedas |
| Cristales de obsidiana | Mejora de armas | Muy raro | 50 monedas |

<div align="center"><p><i>Tabla 7: Materiales para mejorar.</i></p></div>

<br>

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

  
## 5. INTERFAZ  
### 5.1. DIAGRAMA DE FLUJO

A continuación se muestra el diagrama de flujo de las interfaces del juego, donde se representa cómo se conectan entre sí las distintas pantallas y menús.

![Imagen del diagrama de flujo de las interfaces](/Fotos_README/Flujo_de_interfaces.png "Diagrama de flujo de las interfaces")  
<div align="center"><p><i>Imagen 1: Diagrama de flujo de las interfaces</i></p></div>

---

## 6. ARTE

En este apartado se muestran los elementos visuales principales del juego, aquellos que aportan un estilo y características únicas.  


### 6.1. ARTE 2D  

#### 6.1.1. CONCEPTS

<div align="center"><img src="/_Project/Assets/Concept_art/Objetos_personaje.png" alt="Objetos que lleva el personaje encima" width="400"></img></div>  
<div align="center"><p><i>Imagen 2: Diseño de los objetos que lleva el personaje encima</i></p></div>  
<br><br>
<div align="center"><img src="/_Project/Assets/Concept_art/Turnaround_topo.PNG" alt="Diseño del protagonista" width="400"></img></div> 
<div align="center"><p><i>Imagen 3: Diseño del protagonista</i></p></div>
<br><br>
<div align="center"><img src="/_Project/Assets/Concept_art/Turnaround_vagon.PNG" alt="Diseño de la vagoneta" width="400"></img></div>  
<div align="center"><p><i>Imagen 4: Diseño de la vagoneta</i></p></div>
<br><br>
<div align="center"><img src="/_Project/Assets/Concept_art/Turnaround_barril.png" alt="Diseño del barril" width="400"></img></div>  
<div align="center"><p><i>Imagen 5: Diseño del barril</i></p></div>
<br><br>
<div align="center"><img src="/_Project/Assets/Concept_art/Turnaround_cajas.PNG" alt="Diseño de las cajas" width="400"></img></div>  
<div align="center"><p><i>Imagen 6: Diseño de las cajas</i></p></div>
<br><br>
<div align="center"><img src="/_Project/Assets/Concept_art/Turnaround_golem.png" alt="Diseño del golem" width="400"></img></div>  
<div align="center"><p><i>Imagen 7: Diseño del golem</i></p></div>
<br><br>
<div align="center"><img src="/_Project/Assets/Concept_art/Turnaround_rocas.png" alt="Diseño del primer set de rocas" width="400"></img></div>  
<div align="center"><p><i>Imagen 8: Diseño del primer set de rocas</i></p></div>
<br><br>
<div align="center"><img src="/_Project/Assets/Concept_art/Turnaround_rocas2.PNG" alt="Diseño del segundo set de rocas" width="400"></img></div>  
<div align="center"><p><i>Imagen 9: Diseño del segundo set de rocas</i></p></div>
<br><br>

### 6.2. MÚSICA Y EFECTOS DE SONIDO

El apartado sonoro busca reforzar la atmósfera del juego y acompañar la experiencia del jugador. La música y los efectos de sonido están diseñados para ser coherentes con la estética visual y narrativa del juego.  

#### 6.3.1. MÚSICA

* Música ambiental para el pueblo.
* Música ambiental para la mina.
* Música para la tienda.
* Música para combate contra enemigos.

#### 6.3.2. EFECTOS DE SONIDO

* Sonido de jugador caminando.
* Sonido de jugador atacando.
* Sonido de enemigo muerto.

---

### 7. EQUIPO

**Lara Sánchez Sanz:** Modelado y animación 3D de personajes.  
**Jesús Mercado Rioja:** Diseño 2D y modelado 3D de escenarios.  
**Ana María Caamaño del Amo:** Modelado 3D de props.  
**Javier Martín Mulero:** Programación de mecánicas principales.  
**Unai Pastrana Torres:** Diseño y programación de interfaces.  
**Santiago Varela Rey:** Música y efectos de sonido.

---

### 8. POSTMORTEM
#### 8.1. INDIVIDUAL
##### 8.1.1. QUÉ SALIÓ BIEN

**Ana María Caamaño del Amo:** se ha avanzado en el desarrollo del juego de manera constante, lo que ha permitido que para la entrega de la alfa esté implementado todo lo que queríamos. Respecto al modelado, la comunicación con el concept artist fue bastante fluida, por lo que los modelados pudieron comenzarse a realizar temprano en el desarrollo. 

**Javier Martín Mulero:** se han cumplido los plazos establecidos por el equipo, llegando a hacer incluso más de lo que se había pensado en un principio para la alfa. El desarrollo del juego en todos sus apartados está siendo correcto.

**Lara Sánchez Sanz:** la organización del equipo fue bastante efectiva desde el inicio, lo que nos ha permitido completar la entrega alfa sin problemas. La comunicación entre todos los miembros del equipo en general fue bastante fluida y continua, el Teams se ha usado de manera constante para compartir la información de los avances del equipo. Para ser el primer trabajo completo en 3D del grupo, el equipo se adaptó bastante bien al proceso, aprendiendo sobre la marcha.

**Jesús Mercado Rioja:** se ha desarrollado el juego de forma correcta en base a los objetivos planteados al comienzo del proyecto para la fase alfa, en este sentido el equipo supo cómo dar prioridad a unas tareas sobre otras, permitiendo alcanzar estos objetivos de forma más rápida. Respecto al apartado artístico del juego, esto se aplicó tanto en el diseño como en el modelado 3D de los diferentes personajes, props, etc. Destacando la importancia de unos sobre otros, y por tanto marcando cuáles de ellos se debían desarrollar primero.

**Unai Pastrana Torres:**
En la alfa dió tiempo a programar las mecánicas principales (movimiento, salud y venta de objetos, así como un buen feedback visual en las interfaces.
Además, se realizó la persistencia básica de la información de partida.
Se logró un apartado artístico definido, así como la actividad básica en redes sociales.

**Santiago Varela Rey:** la selección y edición de efectos de sonido y música se ha supervisado por el resto del equipo y se ha recibido feedback por parte de compañeros para que elementos como la música, sean más acordes a la estética cartoon del producto.


##### 8.1.2. QUÉ SALIÓ MAL

**Ana María Caamaño del Amo:** no se usó de forma correcta Trello para el seguimiento de tareas, se usó decentemente al comienzo del desarrollo y según se fue avanzando su uso decayó: surgieron nuevas tareas que no se incluyeron, no se marcaron qué tareas estaban en desarrollo y cuáles acabadas, etc. Lo que provocó que miembros del equipo tuviesen que ir preguntando personalmente al resto. Otra cosa serían las redes sociales, en las cuales se comenzaron a publicar cosas de forma tardía y esporádica cuando se debería haber hecho de forma más constante.

**Javier Martín Mulero:** no considero que haya una mala organización pero si hay una mala comunicación entre todos los miembros del equipo. Además hay elementos de diseño que se han cambiado a mitad de la alfa. Por último, hay miembros del equipo que no han usado del todo bien la tabla de Trello que tenemos para organizar las tareas para la alfa, olvidándose de marcarlas como completadas una vez hechas o una vez empezadas a hacer no ponerlas como que se estaban haciendo.

**Lara Sánchez Sanz:** aunque al inicio había una comunicación entre todos los miembros, también hubo cierta falta de seguimiento en tareas compartidas, no utilizando correctamente y de manera constante las herramientas para ello como Trello. En cuanto a la narrativa, faltó tiempo para dramatizar mejor ciertos eventos y reforzar la atmósfera en algunas salas.

**Jesús Mercado Rioja:** si bien se consiguieron acabar los objetivos planteados para esta fase, se podrían haber desarrollado muchas más cosas si hubiera habido una mejor comunicación entre los miembros del equipo, ya fuera por mensajes o por  llamadas que permitieran poner en común la opinión de todos sobre diferentes aspectos del juego. Ya que en muchas ocasiones la comunicación se basó en preguntar a personas de forma privada, esto provocado a su vez por esa falta de comunicación grupal notable en el uso incorrecto de Trello, hacía que los integrantes del grupo no tuviéramos claro qué tareas se habían finalizado y cuáles no.

**Unai Pastrana Torres:** 

Se podría haber logrado una mejor organización de tareas en trello, muchas de ellas no las pude marcar hasta el final, porque eran muy genéricas.

Se decidió recortar bastante el sistema de gestión de recursos y si se se decide ampliar a la idea inicial se tendrá que rehacer código y diseño de interfaces.

Tuve que rehacer ciertas cosas en Unity por no plantear el diseño de las interfaces de forma modular con prefabs desde el principio.

**Santiago Varela Rey:** las tareas relacionadas con sonido y música se han realizado, desde mi punto de vista, de forma lenta. Se podría haber optimizado el trabajo y realizado otras tareas del desarrollo si se hubiesen terminado desde el principio.

##### 8.1.3. QUÉ SE PUEDE MEJORAR

**Ana María Caamaño del Amo:** en el apartado de 3D considero que habría que controlar mejor la cantidad de vértices y polígonos que tienen los modelos para asegurarse de que estén lo más optimizados posibles y no lleguen a dar algún problema. También habría que mejorar la comunicación con los miembros de ámbitos diferentes, ya que considero que cada uno solo se centraba en lo que le tocaba y no en los demás aspecto del juego.

**Javier Martín Mulero:** haría que la comunicación fuera diferente; sobre todo, más directa entre los compañeros del equipo. De cara a la beta en la parte del código, intentar mejorarlo ya que hay sistemas del juego que no considero que estén bien programados.Aún así, creo que la alfa ha salido bastante bien a pesar de que ha habido algún problema.

**Lara Sánchez Sanz:** sería útil establecer un mejor sistema de tareas completadas y por hacer, uno que el equipo pueda usar de forma constante. También creo que aunque el ritmo del equipo ha sido bastante bueno, podríamos gestionar mejor el tiempo para la siguiente entrega para seguir trabajando sin retrasos ni problemas de entrega.

**Jesús Mercado Rioja:** se debería mejorar la comunicación del equipo, intentando que esta involucre a todos los miembros del equipo y no solo a aquellos que formen parte del  mismo ámbito del trabajo. En el apartado artístico del juego, se podrían simplificar algunos de los diseños para hacer así que los modelados 3D sean acordes a la idea  low poly del proyecto.

**Unai Pastrana Torres:** 
Definir bien el código y la gestión de prefabs para que resulte fácil hacer cambios que se propaguen rápido por todas las escenas y objetos del proyecto.
Llevar las tareas de trello al día para facilitar la coordinación con los compañeros.

**Santiago Varela Rey:** resultaría más acertado el resolver las tareas de forma más eficaz para evitar acumular otras tareas más importantes para el desarrollo. Para futuras versiones se pretenderá corregir esto.  

##### 8.1.4. CONCLUSIÓN

**Ana María Caamaño del Amo:** a pesar de los problemas de comunicación, la alfa ha salido incluso mejor de lo que se esperaba; lo que va a motivar al equipo a seguir esforzándose y mejorar para asegurarse de que la beta quede lo mejor posible.

**Javier Martín Mulero:** creo que a todos nos ha gustado cómo ha quedado la alfa, más completa de la que lo habíamos planteado. Personalmente, creo que es un impacto positivo en el equipo que haya salido así y de cara a la beta si mejoramos lo que ha salido mal seguro que sale mejor que la alfa.

**Lara Sánchez Sanz:** la entrega alfa ha salido bastante bien, incluso mejor de lo que esperábamos, teniendo en cuenta que es el primer proyecto 3D del equipo. Hemos conseguido una base sólida sobre la que seguir trabajando, y ahora partimos de un buen punto para la beta.

**Jesús Mercado Rioja:** la fase alfa ha salido mejor de lo esperado, y a pesar de la falta de comunicación que se ha tenido en algunos momentos, el equipo ha sabido resolver cualquier problema que iba surgiendo para obtener un resultado acorde con los objetivos marcados. Además, esto permitirá al equipo partir de una base sólida y una experiencia previa que ayudará a mejorar la eficiencia del desarrollo durante la fase beta.

**Unai Pastrana Torres:** el desarrollo avanzó en jugabilidad y arte, pero la falta de una planificación detallada afectó al flujo de trabajo.

**Santiago Varela Rey:** estoy satisfecho con el resultado de la versión alfa. Cada uno hemos realizado las tareas correspondientes y la comunicación entre los miembros ha agilizado el desarrollo y uniformizado el concepto del videojuego.  

#### 8.2. GRUPAL
##### 8.2.1. QUÉ SALIÓ BIEN

El equipo ha logrado avanzar de forma constante y cumplir los objetivos de la entrega alfa. La organización inicial fue efectiva, con un reparto de tareas claro y una comunicación fluida, se usó Teams de forma constante para compartir avances y coordinar el trabajo.

A nivel técnico, se implementaron las mecánicas principales y se logró el estilo artístico esperado en los modelos 3D.  

##### 8.2.2. QUÉ SALIÓ MAL

Uno de los principales problemas fue el uso de Trello. Aunque se utilizó al principio, su seguimiento decayó con el tiempo: no se registraron nuevas tareas, no se marcaban como completadas; y esto obligó a resolver dudas de forma individual. La comunicación grupal se vio afectada, con decisiones tomadas en privado y falta de reuniones que permitieran poner ideas en común.


##### 8.2.3. QUÉ SE PUEDE MEJORAR

De cara a la beta, es necesario reforzar la comunicación entre áreas y mantener un sistema de seguimiento de tareas constante y actualizado. 

A nivel técnico se propone mejorar ciertos elementos como el uso de prefabs y la modularidad del código. A nivel artístico, se recomienda controlar mejor la optimización de los modelos 3D.  

##### 8.2.4. CONCLUSIÓN

A parte de los problemas de comunicación y organización, la versión alfa ha superado las expectativas del equipo. Se ha conseguido una base sólida sobre la que seguir trabajando, y el resultado ha motivado al grupo a continuar con energía y compromiso. Si se aplican las mejoras detectadas, la beta tiene potencial para ser aún más completa y pulida.

