% Transformujemy uk³ad sto³u na uk³ad PP
Y1=makehgtform('translate',[1900 1900 0],'zrotate',-pi/2,'xrotate',pi); 
% Tworzymy uk³ad robota na podstawie uk³adu sto³u i robimy z niego uk³ad
% prawoskrêtny
Y3=makehgtform('translate',[TRU1 TRU2 0],'zrotate',TRU3,'xrotate',pi);
% Transformujemy uk³ad robota do uk³adu PP
OI=Y1*Y3;
% Wyci¹gamy wspó³rzêdne X i Y
TRU1=OI(1,4);
TRU2=OI(2,4);
%% Wyliczamy orientacjê
X=OI(2,1)*1;
Y=OI(1,1)*1;
Omega=atan2(X,Y); % Tê wartoœæ bêdziemy wrzucaæ do funkcji controller by zdobyæ prêdkoœci robota
%% Dodatkowo jeœli byœmy chcieli znaæ k¹t w stopniach
Omega1=atan2d(X,Y);
if(Omega1<0)
   Omega1=Omega1+360;  
end
