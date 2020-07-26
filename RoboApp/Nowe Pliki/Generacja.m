%% Zmieniamy rozmiar wykresu
axis([0 1900 0 1900])
set(gca,'XColor', 'none','YColor','none');
set(gca,'xtick',[])
set(gca,'ytick',[])
pbaspect([1 1 1 ])
[imageData, alpha] = export_fig(gcf);%Pobieram zdjêcie wykresu
imwrite(imageData,"JK.bmp"); %Zapisuje go do pliku
close all

IMAGE_SIZE = 1 * 1900;
POINT_FREE_ZONE = 47.5;

clear droga; %Czyœcimy macierz ze œcie¿k¹
X_We=0;%Wektor ze wspó³rzêdnymi X wêz³ów
Y_We=0;%Wektor ze wspó³rzêdnymi Y wêz³ów
Inkre_1=1;%Zmienna wektorowa ikrementowana do wype³niania macierzy
Inkre_2=1;%Zmienna wektorowa ikrementowana do wype³niania macierzy

%Pierwszy wêze³ wype³niamy wspólrzêdnymi robota
wezly(1,1)=1;
wezly(1,2)=X; % Pobrane z visual studio
wezly(1,3)=Y; % Pobrane z visual studio
%Ostatni wêze³ wype³niami wspó³rzêdnymi wspó³rzêdnymi punktu koñcowego
wezly(50,1)=50;
wezly(50,2)=Xk; % Pobrane z visual studio
wezly(50,3)=Yk; % Pobrane z visual studio

%% Sprawdzamy czy te dwa wêz³y nie znajduj¹ siê przy krawêdziach sto³u, gdzie robot nie ma dostêpu, jeœli tam s¹ skrypt jest przerywany i zwracane jest 1 w zmiennej, do której zapisywana jest œcie¿ka
if wezly(1,2,:)< POINT_FREE_ZONE || ...
   wezly(1,3,:)< POINT_FREE_ZONE || ...
   wezly(1,2,:)> IMAGE_SIZE - POINT_FREE_ZONE || ...
   wezly(1,3,:)> IMAGE_SIZE - POINT_FREE_ZONE
    droga=1;
    return;
end
if wezly(50,2,:) < POINT_FREE_ZONE || ... 
   wezly(50,3,:)< POINT_FREE_ZONE || ...
   wezly(50,2,:)> IMAGE_SIZE - POINT_FREE_ZONE || ...
   wezly(50,3,:)> IMAGE_SIZE - POINT_FREE_ZONE 
   
    droga=1;
    return;
end
%% Losujemy wêz³y
for J=2:(size(wezly,1)-1)
   
   wezly(J)=J;
   wezly(J,2,:)=IMAGE_SIZE*rand(1,1); 
   wezly(J,3,:)=IMAGE_SIZE*rand(1,1);
   
   while(wezly(J,2,:)< POINT_FREE_ZONE || ...
        wezly(J,3,:)< POINT_FREE_ZONE || ...
        wezly(J,2,:)> IMAGE_SIZE - POINT_FREE_ZONE || ...
        wezly(J,3,:)> IMAGE_SIZE - POINT_FREE_ZONE)
        
        wezly(J,2,:)= IMAGE_SIZE * rand(1); 
        wezly(J,3,:)= IMAGE_SIZE * rand(1); 
          
    end
      
end



%% Przetwarzamy obraz
Obraz=imread('JK.bmp'); %%Wczytanie obraz wykresu
SE = strel('octagon',9);
IMBI=imbinarize(Obraz); %%Binaryzacja
IMDIL=imdilate(~IMBI,SE); %%Dylatacja
IMDIL = imresize(IMDIL,[IMAGE_SIZE IMAGE_SIZE]); %%Zmiana rozmiaru
hold on
[kra]=wszystko(IMDIL); % T¹ funkcj¹ pobieramy wspó³rzêdne przeszkód z wygenerowanego obrazu
%% Rysujemy przeszkody
for(i=1:size(kra,1))
    K=kra{i,1};
plot(K(:,2),K(:,1));
end
 %% Sprawdzamy, czy punkt koñcowy lub pocz¹tkowy znajduj¹ siê w przeszkodzie, jeœli tak to funkcja jest przerywana i oddaje nam jedynkê w zmiennej œcie¿ki
Inkre_3=1; %Zmienna inkrementacyjna
while(Inkre_3<size(kra,1)+1)
K=kra{Inkre_3,1};
in = inpolygon(wezly(1,2),wezly(1,3),K(:,2),K(:,1));  
Inkre_3=Inkre_3+1;
if(in==1)
    droga=1;
    return;
end
end
 Inkre_3=1;
 while(Inkre_3<size(kra,1)+1)
    K=kra{Inkre_3,1};
in = inpolygon(wezly(50,2),wezly(50,3),K(:,2),K(:,1)); 
Inkre_3=Inkre_3+1;
if(in==1)
    droga=1;
    return;
end
 end
 %% Sprawdzamy, które proste miêdzy wêz³ami przecinaj¹ siê z przeszkodami
 Dopusz=0; % Zmienna do sprawdzania, czy nowo utworzony wêze³ jest dopuszczalny (1 jest niedopuszczalny, 0 jest dopuszczalny)
for i=1:size(wezly,1)
   while(Inkre_2<size(kra,1)+1)
    K=kra{Inkre_2,1};
    in = inpolygon(wezly(i,2),wezly(i,3),K(:,2),K(:,1));  

    if(in==1) %Jeœli przecina, to losujemy nowy punkt i sprawdzamy czy nie jest zawarty w niedopuszczalnym obszarze
             
    wezly(i,2)= IMAGE_SIZE * rand(1,1);
    wezly(i,3)= IMAGE_SIZE *rand(1,1);
    while(wezly(i,2,:)< POINT_FREE_ZONE || ...
          wezly(i,3,:)< POINT_FREE_ZONE || ...
          wezly(i,2,:)> IMAGE_SIZE - POINT_FREE_ZONE || ...
          wezly(i,3,:)> IMAGE_SIZE - POINT_FREE_ZONE)
       
         wezly(i,2)= IMAGE_SIZE * rand(1); 
         wezly(i,3)= IMAGE_SIZE * rand(1); 
    end
    Dopusz=1;
    Inkre_2=1;
    end
    if(Dopusz==0) %Jeœli zmienna jest równa 0 sprawdzamy kolejny wêze³
    Inkre_2=Inkre_2+1;
    end
    Dopusz=0;
    end
      
plot(wezly(i,2),wezly(i,3),'bo','LineWidth',10);
Inkre_2=1;

end
%% Tworzymy listê pokazuj¹c¹, które wêz³y maj¹ do siebie dostêp
ZAJETE=0; % Zmienna informuj¹ca, czy linia miêdzy jednym wêz³em a drugim nie przechodzi przez przeszkodê(1 jeœli przechodzi, 0 jeœli nie)
for i=1:size(wezly,1)

   for y=1:size(wezly,1)
        
       for N=1:size(kra,1)
             K=kra{N,1};
             [x_przeciecia,y_przeciecia] = polyxpoly([wezly(i,2),wezly(y,2)],[wezly(i,3),wezly(y,3)],K(:,2),K(:,1));
       if numel(x_przeciecia) > 1     
          ZAJETE=1; % Jeœli prosta siê przecina zamieniamy zmienn¹ "ZAJETE" na 1 i nie dodajemy tej pary do listy
       end
       end
       if ZAJETE==1
       
       else 
   plot([wezly(i,2),wezly(y,2)],[wezly(i,3),wezly(y,3)],'k');
   X_We(Inkre_1)=wezly(i,1);
   Y_We(Inkre_1)=wezly(y,1);
   Inkre_1=Inkre_1+1;
       end
       ZAJETE=0;
   end
end

segments = [(1:size(X_We,2)); Y_We; X_We]'; %Tworzymy macierz z po³¹czeniami wêz³ów, która zostanie u¿ywa w algorytmie

[~,Punkty_sciezki] = dijkstra(wezly, segments, 1, 50); % Puszczamy algorytm dijkstry

close all
%% Wype³niamy macierze wartoœciami otrzymanymi z algorytmu
    for(P_drogi=1:size(Punkty_sciezki,2))
       Punkt=[wezly(Punkty_sciezki(P_drogi),2) wezly(Punkty_sciezki(P_drogi),3)]; 
       % Tworzymy macierz œcie¿ki dla Pure Pursuita(transfomujemy)
       droga(P_drogi,1)=Punkt(2);
       droga(P_drogi,2)=1900-Punkt(1);
       % Tworzymy macierz œcie¿ki dla wizualizacji
       droga_wiz(P_drogi,1)=POIP(1);
       droga_wiz(P_drogi,2)=POIP(2);
       
    end
%% Przygotowujemy kontroler
controller = robotics.PurePursuit;
controller.Waypoints = droga;
controller.DesiredLinearVelocity =25;
controller.MaxAngularVelocity =10;
controller.LookaheadDistance =0.4;

